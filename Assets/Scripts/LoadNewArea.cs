using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewArea : MonoBehaviour
{
    public string sceneToLoad;
    public void LoadArea(){
        movePlayerStartPoint();
        Application.LoadLevel(sceneToLoad);
    }
	
	void OnTriggerEnter2D(Collider2D other){
       if(other.gameObject.CompareTag("Player")){
         Application.LoadLevel(sceneToLoad);
        }
	}

    private void movePlayerStartPoint(){
        PlayerStartPoint point = FindObjectOfType<PlayerStartPoint>();
        PlayerController player = FindObjectOfType<PlayerController>();
        if (point == null || player == null){
            return;
        }
        point.transform.position = player.transform.position;
    }
}
