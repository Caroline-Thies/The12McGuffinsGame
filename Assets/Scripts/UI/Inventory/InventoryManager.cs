using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour {

    public List<InventoryItem> items = new List<InventoryItem>();

    public Canvas canvas;
    public GameObject slotContainer;
    public GameObject itemSlotPrefab;

    private class SlotInstance {
        public GameObject uiSlot;
        public InventoryItem item;
    }

    private List<SlotInstance> slotInstances = new List<SlotInstance>();
    private HashSet<String> itemsInPossession = new HashSet<String>();

    void Start() {
        canvas.enabled = false;

        RectTransform containerTransform = (RectTransform) slotContainer.transform;
        RectTransform slotTransform = (RectTransform) itemSlotPrefab.transform;

        double rawItemsPerRow = containerTransform.rect.width / slotTransform.rect.width;
        int itemsPerRow = (int) Math.Floor(rawItemsPerRow);

        double offsetLeftRelative = (rawItemsPerRow - itemsPerRow) / 2.0f;
        float offsetLeft = (float) (slotTransform.rect.width * offsetLeftRelative);

        Vector2 oldPivot = containerTransform.pivot;
        containerTransform.pivot = new Vector2(0, 1);

        float slotWidth = slotTransform.rect.width;
        float slotHeight = slotTransform.rect.height;

        int index = 0;
        items.ForEach((item) => {
            GameObject slot = InitSlot(item);
            slot.transform.SetParent(slotContainer.transform);

            SlotInstance inst = new SlotInstance();
            inst.uiSlot = slot;
            inst.item = item;
            slotInstances.Add(inst);
            
            RectTransform rect = (RectTransform) slot.transform;
            rect.localPosition = Vector3.zero;
            rect.localScale = Vector3.one;
            rect.pivot = new Vector2(0, 1);

            int row = index / itemsPerRow;
            int column = index % itemsPerRow;
            rect.localPosition = new Vector2(offsetLeft + column * slotWidth, -slotHeight * row);

            index += 1;
        });

        containerTransform.pivot = oldPivot;
        updateSlots();
    }

    public void GiveItem(string identifier) {
        checkItemIdentifier(identifier);
        itemsInPossession.Add(identifier);
        updateSlots();
    }

    public void TakeItem(string identifier) {
        checkItemIdentifier(identifier);
        itemsInPossession.Remove(identifier);
        updateSlots();
    }

    public bool HasItem(string identifier) {
        checkItemIdentifier(identifier);
        return itemsInPossession.Contains(identifier);
    }

    public void Show() {
        canvas.enabled = true;
    }

    public void Hide() {
        canvas.enabled = false;
    }

    public bool IsShown() {
        return canvas.enabled;
    }

    private GameObject InitSlot(InventoryItem item) {
        GameObject slot = Instantiate(itemSlotPrefab);

        GameObject imageObj = slot.transform.Find("ItemSprite").gameObject;
        Image image = imageObj.GetComponent<Image>();
        image.sprite = item.sprite;

        return slot;
    }

    private void checkItemIdentifier(string identifier) {
        bool found = false;

        foreach (SlotInstance slotInst in slotInstances) {
            if (slotInst.item.identifier == identifier) {
                found = true;
                break;
            }
        }

        if (!found) {
            throw new Exception("No item with identifier '" + identifier + "'was found.");
        }
    }

    private void updateSlots() {
        slotInstances.ForEach(inst => {
            bool isVisible = itemsInPossession.Contains(inst.item.identifier);

            GameObject imageObj = inst.uiSlot.transform.Find("ItemSprite").gameObject;
            Image image = imageObj.GetComponent<Image>();

            if (isVisible) {
                image.sprite = inst.item.sprite;
            } else {
                image.sprite = inst.item.hiddenSprite;
            }
        });
    }

}