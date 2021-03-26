using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyEventManager : MonoBehaviour
{
    private static List<string> triggeredEvents = new List<string>();

    public static void triggerEvent(string eventName){
        bool eventTriggered = isEventTriggered(eventName);
        if(!eventTriggered){
            triggeredEvents.Add(eventName);
        }
    }

    public static bool isEventTriggered(string eventName){
        return triggeredEvents.Contains(eventName);
    }
}
