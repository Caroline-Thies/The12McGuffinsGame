﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private bool uiManagerExists=false;
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
