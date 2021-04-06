using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddInventoryItem : MonoBehaviour
{
    public string itemId;


    public void AddItem(){
        InventoryManager invManager = FindObjectOfType<InventoryManager>();
        if (!invManager.HasItem(itemId)) {
            invManager.GiveItem(itemId);
        }
    }
}
