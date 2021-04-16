using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewArea : MonoBehaviour
{
    public string sceneToLoad;
	
    public void LoadArea(){

        Application.LoadLevel(sceneToLoad);
    }
	
	public void OnTriggerEnter2D(Collider2D other){
       if(other.gameObject.CompareTag("Player")){
		    DestroyEnemy enemyDestory = gameObject.GetComponent<DestroyEnemy>();
			if(enemyDestory != null){
				BattleSystem.currentEnemy = enemyDestory.getEnemyID();
			}
			Application.LoadLevel(sceneToLoad);
        }
	}

}
