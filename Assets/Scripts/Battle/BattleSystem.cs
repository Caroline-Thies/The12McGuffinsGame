using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

enum State {
	Begin,
	NextEnemy,
	PlayerSelectAction,
	PlayerSelectsWeapon,
	EnemyAttacks,
	PlayerWon,
	PlayerLost,
};

public class BattleSystem : MonoBehaviour
{
	public static BattleInfo currentBattle;

	[Header("Selection Dialogue")]
	public Text dialogueText;
	public GameObject scrollContent;
	public GameObject selectionItemPrefab;
	public Color highlightedColor;

	[Header("HUD")]
	public Image enemySprite;
	public Text enemyNameText;
	public Slider playerHealthSlider;
	public Slider enemyHealthSlider;

	private Dictionary<Weapon, uint> weaponUsages = new Dictionary<Weapon, uint>();
	
	private State currentState = State.Begin;
	private State? stateLastFrame = null;

	private UnitInstance currentUnit = null;
	private float playerHealth = 100f;

	private DialogueContent currentDialogue = null;

	class SelectionEvent : UnityEvent<int> {}

	class DialogueContent {
		public SelectionEvent selectionEvent;
		public int index = 0;
		public List<string> values;
		public string text;
		
		public DialogueContent(string text, List<string> values) {
			this.selectionEvent = values == null ? null : new SelectionEvent();
			this.text = text;
			this.values = values;
		}
	}

    void Start()
    {
		InventoryManager invManager = FindObjectOfType<InventoryManager>();

		// Find all items the player may use for the fight
		List<InventoryItem> availableWeapons = invManager.items.FindAll(item => {
			return item.tag == "weapon" && invManager.HasItem(item.identifier);
		});

		if (availableWeapons.Count == 0) {
			throw new Exception("The player doesn't have a weapon in their inventory. This should not happen.");
		}

		// Initialize the maximum usages for all available weapons
		availableWeapons.ForEach(item => {
			Weapon weapon = (Weapon) item;
			weaponUsages.Add(weapon, weapon.numberOfUsesPerFight > 0 ? (uint) weapon.numberOfUsesPerFight : 1);
		});
    }

	private void Update() {
		HandleDialogueInput();

		// This ensures that our transition code only gets run
		// for a single frame after the state has changed.
		if (currentState != stateLastFrame) {
			stateLastFrame = currentState;
			OnStateChange();
		}
	}

	private void OnStateChange() {
		switch (currentState) {
			case State.Begin: {
				currentState = State.NextEnemy;
				break;
			}

			case State.NextEnemy: {
				Unit nextUnit = currentBattle.NextUnit();

				if (nextUnit == null) {
					currentBattle.MarkAllEnemiesAsDead();
					currentState = State.PlayerWon;
				} else {
					currentUnit = new UnitInstance(nextUnit);
					currentState = State.PlayerSelectAction;
					UpdateHUD();
				}

				break;
			}

			case State.PlayerSelectAction: {
				List<string> options = new List<string>();
				options.Add("Fight");
				options.Add("Run");

				SetDialogueContent("What do you want to do?", options).AddListener(index => {
					if (index == 1) {
						currentState = State.PlayerLost;
					} else {
						currentState = State.PlayerSelectsWeapon;
					}
				});

				break;
			}

			case State.PlayerSelectsWeapon: {
				List<string> names = new List<string>();
				List<Weapon> selectableWeapons = new List<Weapon>();

				foreach (KeyValuePair<Weapon, uint> entry in weaponUsages) {
					Weapon weapon = entry.Key;
					uint usages = entry.Value;

					if (weapon.numberOfUsesPerFight == 0) {
						names.Add(weapon.name);
						selectableWeapons.Add(weapon);
					} else if (usages > 0) {
						string text = string.Format("{0} ({1})", weapon.name, usages);
						names.Add(text);
						selectableWeapons.Add(weapon);
					}
				}

				SetDialogueContent("Select a weapon", names).AddListener(index => {
					Weapon selectedWeapon = selectableWeapons[index];

					if (PlayerAttacksWithWeapon(selectedWeapon)) {
						string message = String.Format("'{0}' was killed", currentUnit.unit.name);
						SetDialogueContent(message, null);
						SetStateDelayed(State.NextEnemy, 1.2f);
					} else {
						currentState = State.EnemyAttacks;
					}
				});

				break;
			}

			case State.EnemyAttacks: {
				SetDialogueContent("The enemy attacks...", null);
				StartCoroutine(OnEnemyAttack());
				break;
			}

			case State.PlayerWon: {
				List<string> options = new List<string>();
				options.Add("Continue");

				SetDialogueContent("Congratulations! you won the battle", options).AddListener(_ => {
					ReturnToPreviousScene();
				});

				break;
			}

			case State.PlayerLost: {
				List<string> options = new List<string>();
				options.Add("Continue");

				SetDialogueContent("Damn, looks like you lost. Tough luck", options).AddListener(_ => {
					ReturnToBedroom();
				});

				break;
			}
		}
	}

	// We need to call this as a coroutine to add some delay
	private IEnumerator OnEnemyAttack() {
		yield return new WaitForSeconds(0.8f); 

		if (PlayerTakesDamage(currentUnit.unit)) {
			currentState = State.PlayerLost;
		} else {
			SetStateDelayed(State.PlayerSelectAction, 1.2f);
		}
	}

	private bool PlayerTakesDamage(Unit fromUnit) {
		Armor armor = new Armor();
		armor.bluntMultiplier = 1;
		armor.piercingMultiplier = 1;

		playerHealth -= fromUnit.weapon.calculateDamageGiven(armor);
		UpdateHUD();

		return playerHealth <= 0;
	}

	// Applies damage from the given weapon to the current unit.
	// Returns true if the enemy was killed by the attack.
	private bool PlayerAttacksWithWeapon(Weapon weapon) {
		uint currentUsages;

		if (weaponUsages.TryGetValue(weapon, out currentUsages)) {
			// If numberOfUsesPerFight is zero we know that the weapon is supposed to have inifite uses
			if (weapon.numberOfUsesPerFight > 0) {
				weaponUsages[weapon] = currentUsages > 0 ? currentUsages - 1 : 0;
			}

			currentUnit.TakeDamage(weapon);
			UpdateHUD();

			return currentUnit.health <= 0;
		}

		return false;
	}

	private void HandleDialogueInput() {
		if (currentDialogue == null || currentDialogue.selectionEvent == null) {
			return;
		}

		int index = currentDialogue.index;

		if (Input.GetKeyDown(KeyCode.S)) {
			currentDialogue.index = Math.Min(index + 1, currentDialogue.values.Count - 1);	
			RenderDialogue(currentDialogue);
		} else if (Input.GetKeyDown(KeyCode.W)) {
			currentDialogue.index = Math.Max(0, index - 1);
			RenderDialogue(currentDialogue);
		} else if (Input.GetKeyDown(KeyCode.E)) {
			SelectionEvent ev = currentDialogue.selectionEvent;
			currentDialogue = null;
			ev.Invoke(index);
		}
	}

	private void UpdateHUD() {
		playerHealthSlider.maxValue = 100.0f;
		playerHealthSlider.value = Mathf.Max(0.0f, playerHealth);

		enemyHealthSlider.maxValue = currentUnit.unit.maxHealth;
		enemyHealthSlider.value = Mathf.Max(0.0f, currentUnit.health);

		enemyNameText.text = currentUnit.unit.name;
		enemySprite.sprite = currentUnit.unit.battleSprite;
	}

	private SelectionEvent SetDialogueContent(string text, List<string> selectionValues) {
		currentDialogue = new DialogueContent(text, selectionValues);
		RenderDialogue(currentDialogue);
		return currentDialogue.selectionEvent;
	}

	// Render the given DialogueContent instance onto the canvas.
	// Beware: This method does not care about the currently active
	//		   dialogue _at all_. It could lead to some weird bugs
	//		   if you call it outside of SetDialogueContent.
	private void RenderDialogue(DialogueContent content) {
		dialogueText.text = content.text;

		// If the caller did not specify any selection options we don't need
		// to do anything. In fact, we probably even want to hide the
		// scroll view.
		bool hasSelection = content.values != null && content.values.Count > 0;
		Text[] textChildArray = scrollContent.GetComponentsInChildren<Text>();

		if (hasSelection) {
			List<Text> children = new List<Text>(textChildArray);

			// These two if branches make sure that we have the exact amount
			// of text entries needed to accomodate all selection options
			if (children.Count < content.values.Count) {
				int missing = content.values.Count - children.Count;

				for (int i = 0; i < missing; i++) {
					GameObject inst = Instantiate(selectionItemPrefab);
					inst.transform.SetParent(scrollContent.transform, false);
					children.Add(inst.GetComponent<Text>());
				}
			} else if (children.Count > content.values.Count) {
				int tooMany = children.Count - content.values.Count;

				for (int i = 0; i < tooMany; i++) {
					// We need to remove from top to bottom, otherwise
					// the entries shift and we remove the wrong ones
					int index = children.Count - 1 - i;
					Destroy(children[index].gameObject);
					children.Remove(children[index]);
				}
			}

			int itemsPerPage = 2;
			int activePage = content.index / itemsPerPage;

			// Set text and color for all text entries
			for (int i = 0; i < children.Count; i++) {
				Text child = children[i];
				int page = i / itemsPerPage;

				if (page == activePage) {
					child.enabled = true;
					child.text = content.values[i];
					child.color = content.index == i ? highlightedColor : Color.black;
				} else {
					child.enabled = false;
				}

				if (content.index == i) {
					RectTransform scrollTransform = (RectTransform) scrollContent.transform;
					Vector2 scrollPosition = scrollTransform.anchoredPosition;

					// We make the assumption that all menu items have the same height
					RectTransform childTransform = (RectTransform) child.transform;
					scrollPosition.y = childTransform.rect.height * activePage * itemsPerPage;

					scrollTransform.anchoredPosition = scrollPosition;
				}
			}
		} else {
			foreach (Text child in textChildArray) {
				Destroy(child.gameObject);
			}
		}
	}

	private void SetStateDelayed(State targetState, float delay) {
		StartCoroutine(SetStateDelayedInternal(targetState, delay));
	}

	private IEnumerator SetStateDelayedInternal(State targetState, float delay) {
		yield return new WaitForSeconds(delay);
		currentState = targetState;
	}

	private void ReturnToPreviousScene() {
		LoadNewArea loadArea = gameObject.AddComponent<LoadNewArea>();
		loadArea.sceneToLoad = currentBattle.comingFromScene;
		loadArea.LoadArea();
	}

	private void ReturnToBedroom() {
		ScenesData.resetLastPositions();
		LoadNewArea loadArea = gameObject.AddComponent<LoadNewArea>();
		loadArea.sceneToLoad = "bedroom";
		loadArea.LoadArea();
	}

}

