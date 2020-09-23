using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    private PlayerController player;
    private CameraController camera;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.transform.position = transform.position;
        camera = FindObjectOfType<CameraController>();
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, camera.transform.position.z); //this is necessary as to keep the camera's z position. Otherwise weird behaviour
    }

}
