using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string name = "Tasha";
    public string lastSavePoint = "BEDROOM";

    public Player() { }

    public void SavePlayer(){
        SaveSystem.SavePlayer(this);
    }
}
