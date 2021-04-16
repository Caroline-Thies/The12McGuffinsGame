using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Armor Item", fileName="New Armor")]
public class Armor : InventoryItem
{
    public float bluntMultiplier = 1;
    public float piercingMultiplier = 1;

    void Reset() {
        tag = "armor";
    }

    public float calculateDamageTaken(Weapon fromWeapon) {
        return fromWeapon.calculateDamageGiven(this);
    }
}
