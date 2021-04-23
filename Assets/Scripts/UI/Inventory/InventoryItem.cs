using UnityEngine;

[CreateAssetMenu(menuName="Inventory Item", fileName="New Inventory Item")]
public class InventoryItem : ScriptableObject {
    public string identifier;
    public string tag = "misc";
    public Sprite sprite;
    public Sprite hiddenSprite;
}