﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
	LoadNewArea loadNew;
	DestroyEnemy killEnemy;
	
	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	public Text dialogueText;

	[SerializeField] GameObject actionSelector;
	[SerializeField] List<Text> actionText;
	[SerializeField] Color highlightedColor;
	
	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	Unit playerUnit;
	Unit enemyUnit;

	public BattleState state;
	
	int currentAction;
	public static string currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
		loadNew = gameObject.AddComponent<LoadNewArea>() as LoadNewArea;
		state = BattleState.START;
		StartCoroutine(SetupBattle());

			Debug.Log(currentEnemy);

    }

	IEnumerator SetupBattle()
	{
		GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
		playerUnit = playerGO.GetComponent<Unit>();

		GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
		enemyUnit = enemyGO.GetComponent<Unit>();

		dialogueText.text = "The fight against " + enemyUnit.unitName + " begins...";

		playerHUD.SetHUD(playerUnit);
		enemyHUD.SetHUD(enemyUnit);

		yield return new WaitForSeconds(2f);

		state = BattleState.PLAYERTURN;
		PlayerTurn();

	}

	
	void PlayerTurn()
	{
		dialogueText.text = "Choose an action...";
		actionSelector.SetActive(enabled);

	}

	private void Update(){
		if(state == BattleState.PLAYERTURN){
			HandleActionSelection();
		}
	}

	
	void HandleActionSelection(){
		if(Input.GetKeyDown(KeyCode.S)){
			if(currentAction < 1)
			++currentAction;
		} else if(Input.GetKeyDown(KeyCode.W)){
			if(currentAction > 0)
			--currentAction;
		}
		
		UpdateActionSelection(currentAction);
		
		if(Input.GetKeyDown(KeyCode.E)){
			if(currentAction == 0){
				// fight
				if (state != BattleState.PLAYERTURN)
					return;

				StartCoroutine(PlayerAttack());
			} else if(currentAction == 1){
				
			loadNew.sceneToLoad = "Maze";
			loadNew.LoadArea();
				
				//
				//
				//
				//run
				//
				//
				//
				//
				
				
			}
			
		}
		
	}

	void UpdateActionSelection(int selectedAction){
		for(int i = 0;i<actionText.Count; ++i){
			if(i == selectedAction){
				actionText[i].color = highlightedColor;
			} else {
				actionText[i].color = Color.black;
			}
		}
	}
	
	IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
			EnemyDieController();

		


			loadNew.sceneToLoad = "Maze";
			loadNew.LoadArea();
			
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}
	
	public void EnemyDieController(){
		
		/*	if(currentEnemy == "Monster 1"){
				killEnemy.M1isDead = true;
			} else if(currentEnemy == "Monster 2"){
				killEnemy.M2isDead = true;
			} else if(currentEnemy == "Monster 3"){
				killEnemy.M3isDead = true;
			} */
			DestroyEnemy.deadEnemies.Add(BattleSystem.currentEnemy);
	}
	
	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}
	
	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
		
		//
		//
		//
		// Ende
		//
		//
		//
	}
	
	
	
	
}

