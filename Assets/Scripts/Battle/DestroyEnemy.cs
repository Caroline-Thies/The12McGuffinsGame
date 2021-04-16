using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DestroyEnemy : MonoBehaviour
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
}
