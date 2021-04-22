using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ConditionalTilemap : MonoBehaviour
{
    //showIfEventTriggered must be at least as long as conditionalEvents. There is probably a better way of doing this.
    public string[] eventsToCheck;
    public bool[] showIfEventTriggered;
    public int targetLayer;
    
    void Start()
    {
        bool shown = checkIfShown();
        setLayer(shown);
    }

    private bool checkIfShown(){
        int eventsCount = eventsToCheck.Length;
        int eventBoolsCount = showIfEventTriggered.Length;
        KeyEventManager keyEventManager = FindObjectOfType<KeyEventManager>();
        bool show = true;
        for(int i = 0; i < eventsCount; i++){
            bool triggered = keyEventManager.isEventTriggered(eventsToCheck[i]);
            if (i < eventBoolsCount){ //this is to prevent crashing if the boolList is shorter than the eventList
                show = triggered && showIfEventTriggered[i];
            } else {
                show = triggered;
            }
        }
        return show;
    }
    private void setLayer(bool shown){
        TilemapRenderer tilemapRenderer = this.GetComponent<TilemapRenderer>();
        if(!shown){
            tilemapRenderer.sortingOrder = 0;
        } else {
            tilemapRenderer.sortingOrder = targetLayer;
        }
    }
}
