using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public string name;
    public string saveLocation;
    public int[] inventory

    public PlayerData(Player player){
        name = player.name;
        saveLocation = player.saveLocation;
        inventory = player.inventory;
    }
}
