using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Interactable))]
public class InventoryItemInstance : MonoBehaviour {

    public InventoryItem item;
    Interactable interactable;
    InventoryManager invManager;
    SpriteRenderer spriteRenderer;

    private InventoryItem lastSelectedItem = null;

    void Start() {
        if (Application.isPlaying) {
            invManager = FindObjectOfType<InventoryManager>();
            interactable = GetComponent<Interactable>();

            if (!invManager.HasItem(item.identifier)) {
                LoadSprite();

                interactable.interactAction.AddListener(() => {
                    invManager.GiveItem(item.identifier);
                    Disable();
                });
            } else {
                Disable();
            }
        }
    }

#if UNITY_EDITOR
    void Update() {
        if (Application.isPlaying)
            return;

        if (item != lastSelectedItem) {
            LoadSprite();
            lastSelectedItem = item;
        }
    }
#endif

    void LoadSprite() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.sprite;
    }

    void Disable() {
        spriteRenderer.enabled = false;
        interactable.enabled = false;
        this.enabled = false;
    }

}