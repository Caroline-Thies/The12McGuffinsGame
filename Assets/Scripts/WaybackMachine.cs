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

    void Start()
    {
        loadNew = gameObject.AddComponent<LoadNewArea>() as LoadNewArea;
        invManager = FindObjectOfType<InventoryManager>();
        interactable = GetComponent<Interactable>();

        interactable.interactAction.AddListener(() => {
            if (invManager.HasItem("mcguffin")) {
                loadNew.sceneToLoad = destIfMcGuffinFound;
            } else {
                loadNew.sceneToLoad = destIfMcGuffinNotFound;
            }

            loadNew.LoadArea();
        });
    }
}
