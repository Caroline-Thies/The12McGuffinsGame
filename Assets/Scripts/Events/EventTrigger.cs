using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public string eventName;
    public void triggerEvent()
    {
        FindObjectOfType<KeyEventManager>().triggerEvent(eventName);
    }
}
