using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewArea : MonoBehaviour
{
    public string sceneToLoad;
	public string enemy;
	
    public void LoadArea(){

        Application.LoadLevel(sceneToLoad);
    }
	
	void OnTriggerEnter2D(Collider2D other){
       if(other.gameObject.CompareTag("Player")){
			enemy = gameObject.name;
			Application.LoadLevel(sceneToLoad);
        }
	}

}
