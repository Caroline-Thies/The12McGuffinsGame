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
	
	
	
/*	public GameObject Monster1;
	public GameObject Monster2;
	public GameObject Monster3;
	public bool M1isDead = false;
	public bool M2isDead = false;
	public bool M3isDead = false;
	
    // Start is called before the first frame update
    public void Start()
    {
		InvokeRepeating("Update", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
		if(M1isDead){
			Destroy(Monster1);
		}
		if(M2isDead){
			Destroy(Monster2);
		}
		if(M3isDead){
			Destroy(Monster3);
		}
   }
    */
}
