using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStartPoint : MonoBehaviour
{
    private PlayerController player;
    private CameraController camera;
    // Start is called before the first frame update
    void Start()
    {
        checkLastPlayerLocation();
        player = FindObjectOfType<PlayerController>();
        player.transform.position = transform.position;
        camera = FindObjectOfType<CameraController>();
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z); //this is necessary as to keep the camera's z position. Otherwise weird behaviour
    }

    private void checkLastPlayerLocation(){
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
        Vector3? lastPlayerPosition = ScenesData.lastPlayerTransforms[sceneName];
        if(null == lastPlayerPosition){
            return;
        }
        transform.position = (Vector3)lastPlayerPosition;
    }

}
