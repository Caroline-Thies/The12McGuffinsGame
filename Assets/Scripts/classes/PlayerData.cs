using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public string saveLocation;
    public string name;

    public PlayerData(Player player){
        this.saveLocation = player.lastSavePoint;
        this.name = player.name;
    }
}
