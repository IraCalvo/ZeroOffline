using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class InventoryItem
{
    public string itemName;
    public string itemDesc;
    public int itemMaxStackAmount;
    public Sprite itemSpriteIcon;
    public int currentAmount;
    public int itemIndex;

    public InventoryItem(string itemName, string itemDesc, int itemMaxStackAmount, int currentAmount, Sprite itemSpriteIcon)
    {
        this.itemName = itemName;
        this.itemDesc = itemDesc;
        this.itemMaxStackAmount = itemMaxStackAmount;
        this.currentAmount = currentAmount;
        this.itemSpriteIcon = itemSpriteIcon;
    }
}
