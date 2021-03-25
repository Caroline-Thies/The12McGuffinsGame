using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static bool uiManagerExists=false;
    public InputManager inputManager;
    void Start()
    {
        if(!uiManagerExists){
            uiManagerExists = true;
            DontDestroyOnLoad(transform.gameObject);
         } else {
             Destroy(gameObject);
         }
    }
}
