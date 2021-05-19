using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaybackMachine : MonoBehaviour
{
    LoadNewArea loadNew;
    InventoryManager invManager;
    Interactable interactable;

    public string destIfMcGuffinFound;
    public string destIfMcGuffinNotFound;


    public void teleport() {
        loadNew = gameObject.AddComponent<LoadNewArea>() as LoadNewArea;
        invManager = FindObjectOfType<InventoryManager>();
        interactable = GetComponent<Interactable>();

        if (invManager.HasItem("mcguffin")) {
                Debug.Log(destIfMcGuffinFound);
                loadNew.sceneToLoad = this.destIfMcGuffinFound;
            } else {
                Debug.Log(destIfMcGuffinNotFound);
                loadNew.sceneToLoad = this.destIfMcGuffinNotFound;
            }
            Debug.Log("ScenetoLoad:" + loadNew.sceneToLoad);
            loadNew.LoadArea();
    }
}
