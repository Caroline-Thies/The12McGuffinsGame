using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType{
        Scope,
        Book,
        Boots,
        Homework,
    }

    public ItemType type;
    public int amount;
}
