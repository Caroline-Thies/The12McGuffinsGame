using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
	
	public static HashSet<string> deadEnemies = new HashSet<string>();
	bool shouldDelete = false;
	
	
	public void Start(){
		shouldDelete = deadEnemies.Contains(getEnemyID());
	}
	
	public void Update(){
		if(shouldDelete == true){
			Destroy(gameObject);
		}
		
	}
	
	public string getEnemyID(){
		return SceneManager.GetActiveScene().name + "-" + gameObject.name;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(shouldDelete){
			return;
		}
		
		if(other.gameObject.CompareTag("Player")){
			Debug.Log(getEnemyID());
			BattleSystem.currentEnemy = getEnemyID();
			
			LoadNewArea loadArea = gameObject.AddComponent<LoadNewArea>();
			loadArea.sceneToLoad = "BattleArea";
			loadArea.LoadArea();
		}

	}


}
