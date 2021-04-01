using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEventManager : MonoBehaviour
{
    private List<string> triggeredEvents = new List<string>();

    public void triggerEvent(string eventName){
        bool eventTriggered = isEventTriggered(eventName);
        if(!eventTriggered){
            triggeredEvents.Add(eventName);
            Debug.Log("triggered Event" + eventName);
        }
    }

    public bool isEventTriggered(string eventName){
        bool isTriggered = triggeredEvents.Contains(eventName);
        if (isTriggered)
        {
            Debug.Log("Event " + eventName + " has been triggered");
        } else
        {
            Debug.Log("Event " + eventName + " has not been triggered");
        }
        return triggeredEvents.Contains(eventName);
    }
}
