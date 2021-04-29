using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewArea : MonoBehaviour
{
    public string sceneToLoad;
	
    public void LoadArea(){
        movePlayerStartPoint();
        SceneManager.LoadScene(sceneToLoad);
    }
	
	public void OnTriggerEnter2D(Collider2D other){
       if(other.gameObject.CompareTag("Player")){
         LoadArea();
        }
	}

    private void movePlayerStartPoint(){
        PlayerController player = FindObjectOfType<PlayerController>();
        string sceneName = SceneManager.GetActiveScene().name;
        ScenesData.lastPlayerTransforms[sceneName] = player.transform.position;
    }
}
