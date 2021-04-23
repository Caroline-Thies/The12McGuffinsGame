using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Weapon Item", fileName="New Weapon")]
public class Weapon : InventoryItem
{
    public float bluntDamage = 1;
    public float piercingDamage = 1;

    // This property is not being used yet, but it will come
    // into play when we implement a way for the player
    // to select their desired weapon.
    public uint numberOfUsesPerFight = 3;

    void Reset() {
        tag = "weapon";
    }

    public float calculateDamageGiven(Armor againstArmor) {
        if (againstArmor == null) {
            return bluntDamage + piercingDamage;
        }

        return bluntDamage * againstArmor.bluntMultiplier + piercingDamage * againstArmor.piercingMultiplier;
    }
}
