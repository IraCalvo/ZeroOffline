using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryManager : MonoBehaviour
{
    public static PlayerInventoryManager instance;
    public List<InventoryItem> inventoryList = new List<InventoryItem>();
    public int inventoryMaxSize;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public bool PickupItem(IItem itemToPickup)
    {
        foreach (InventoryItem item in inventoryList)
        {
            if (item.itemName == itemToPickup.ItemName)
            {
                if (item.currentAmount < item.itemMaxStackAmount)
                {
                    item.currentAmount++;
                    return true;
                }
            }
        }

        if (inventoryList.Count < inventoryMaxSize)
        {
            InventoryItem newInventoryItem = new InventoryItem(itemToPickup.ItemName, itemToPickup.ItemDesc, itemToPickup.ItemStackAmount, 1, itemToPickup.InventoryIcon);
            inventoryList.Add(newInventoryItem);
            return true;
        }
        else
        {
            return false;
        }
    }
}
