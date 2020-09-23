using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform playerTransform;
    private static bool cameraExists = false;
    // Start is called before the first frame update
    void Start()
    {
         playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
         if(!cameraExists){
             cameraExists = true;
             DontDestroyOnLoad(transform.gameObject);
         } else {
             Destroy(gameObject);
         }
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    private void FollowPlayer(){
        Vector3 temp = transform.position; //store current own position
        temp.x = playerTransform.position.x;
        temp.y = playerTransform.position.y;
        transform.position = temp;
    }
}
