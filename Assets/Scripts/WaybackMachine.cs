using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaybackMachine : MonoBehaviour
{
    LoadNewArea loadNew;
    InventoryManager invManager;
    Interactable interactable;

    void Start()
    {
        loadNew = new LoadNewArea();
        invManager = FindObjectOfType<InventoryManager>();
        interactable = GetComponent<Interactable>();

        interactable.interactAction.AddListener(() => {
            if (invManager.HasItem("mcguffin")) {
                loadNew.sceneToLoad = "Bedroom";
            } else {
                loadNew.sceneToLoad = "Factory";
            }

            loadNew.LoadArea();
        });
    }
}
