using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
	public static HashSet<string> deadEnemies = new HashSet<string>();

	public Unit unit;

#if UNITY_EDITOR
	// This code is only relevant inside of the editor.
	// We use this variable to determine if the developer
	// has selected a different unit. In that case we update
	// the sprite.
	private Unit lastSelectedUnit = null;
#endif

	private bool shouldDelete = false;
	
	public void Start() {
		LoadSprite();

		if (Application.isPlaying) {
			shouldDelete = deadEnemies.Contains(getEnemyID());
		}
	}
	
	public void Update(){
		if (shouldDelete == true) {
			Destroy(gameObject);
		}

#if UNITY_EDITOR
		if (!Application.isPlaying && lastSelectedUnit != unit) {
			LoadSprite();
			lastSelectedUnit = unit;
		}
#endif
	}

	void LoadSprite() {
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		if (unit != null) {
			spriteRenderer.sprite = unit.sprite;
		}
	}
	
	public string getEnemyID(){
		return SceneManager.GetActiveScene().name + "-" + gameObject.name;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (shouldDelete) {
			return;
		}
		
		if (other.gameObject.CompareTag("Player")) {
			string sceneName = SceneManager.GetActiveScene().name;
			BattleInfo battleInfo = new BattleInfo(sceneName, getEnemyID(), unit);

			BattleSystem.currentBattle = battleInfo;

			LoadNewArea loadArea = gameObject.AddComponent<LoadNewArea>();
			loadArea.sceneToLoad = "BattleArea";
			loadArea.LoadArea();
		}
	}


}
