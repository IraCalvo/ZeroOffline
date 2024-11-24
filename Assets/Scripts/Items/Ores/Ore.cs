using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ore : MonoBehaviour, IItem
{
    [SerializeField] string _itemName;
    [SerializeField] string _itemDesc;
    [SerializeField] int _itemStackAmount;
    [SerializeField] Sprite _inventoryIcon;

    public string ItemName 
    {
        get { return _itemName; }
        set { _itemName = value; }
    }

    public string ItemDesc
    {
        get { return _itemDesc; }
        set { _itemDesc = value; }
    }

    public int ItemStackAmount
    {
        get { return _itemStackAmount; }
        set { _itemStackAmount = value; } 
    }

    public Sprite InventoryIcon
    {
        get { return _inventoryIcon; }
        set { _inventoryIcon = value; }
    }
}
