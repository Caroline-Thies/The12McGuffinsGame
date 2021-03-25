using UnityEngine;

[CreateAssetMenu(menuName="Inventory Item", fileName="New Inventory Item")]
public class InventoryItem : ScriptableObject {
    public string identifier;
    public Sprite sprite;
    public Sprite hiddenSprite;
}