using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    public void TriggerSave()
    {
        FindObjectOfType<Player>().SavePlayer();
    }
}
