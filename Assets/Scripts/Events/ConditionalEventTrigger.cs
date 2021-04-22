using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalEventTrigger : EventTrigger
{
    public string conditionEvent = "";

    public void triggerConditionalEvent(){
        if (FindObjectOfType<KeyEventManager>().isEventTriggered(conditionEvent)){
            triggerEvent();
        }
    }
}
