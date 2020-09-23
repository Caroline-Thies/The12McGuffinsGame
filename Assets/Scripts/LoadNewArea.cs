using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadNewArea : MonoBehaviour
{
    public string sceneToLoad;
    public void LoadArea(){
        Application.LoadLevel(sceneToLoad);
    }
}
