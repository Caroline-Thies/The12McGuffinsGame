using UnityEngine;

public class InventoryItemInstance : MonoBehaviour {

    public InventoryItem item;
    Interactable interactable;
    InventoryManager invManager;
    SpriteRenderer spriteRenderer;

    void Start() {
        invManager = FindObjectOfType<InventoryManager>();
        interactable = GetComponent<Interactable>();

        if (!invManager.HasItem(item.identifier)) {
            spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = item.sprite;

            interactable.interactAction.AddListener(() => {
                invManager.GiveItem(item.identifier);
                Disable();
            });
        } else {
            Disable();
        }
    }

    void Disable() {
        spriteRenderer.enabled = false;
        interactable.enabled = false;
        this.enabled = false;
    }

}