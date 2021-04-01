using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalDialogue : MonoBehaviour
{
    public string conditionalEvent;
    public DialogueTrigger ifTriggered;
    public DialogueTrigger ifNotTriggered;

    public void triggerDialogue()
    {
        bool eventTriggered = FindObjectOfType<KeyEventManager>().isEventTriggered(conditionalEvent);
        if (eventTriggered)
        {
            ifTriggered.TriggerDialogue();
        } else
        {
            ifNotTriggered.TriggerDialogue();
        }
    }
}
