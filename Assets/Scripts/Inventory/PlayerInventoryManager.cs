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
            for (int i = 0; i < inventoryMaxSize; i++)
            {
                inventoryList.Add(null);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        for (int i = 0; i < inventoryMaxSize; i++)
        {
            InventoryUIManager.instance.inventoryUISlots[i].slotIndex = i;
        }
    }

    public bool PickupItem(IItem itemToPickup)
    {
        InventoryItem newInventoryItem = new InventoryItem(itemToPickup.ItemName, itemToPickup.ItemDesc, itemToPickup.ItemStackAmount, 1, itemToPickup.InventoryIcon);
        int firstNullSlot = -1;
        for (int i = 0; i < inventoryList.Count; i++)
        {
            InventoryItem item = inventoryList[i];
            if ((item == null || item.itemName == null) && firstNullSlot == -1)
            {
                firstNullSlot = i;
            }
            else if (item != null && item.itemName == newInventoryItem.itemName)
            {
                if (item.currentAmount < item.itemMaxStackAmount)
                {
                    item.currentAmount++;
                    return true;
                }
            }
        }

        if (firstNullSlot != -1)
        {
            newInventoryItem.itemIndex = firstNullSlot;
            inventoryList[firstNullSlot] = newInventoryItem;
            InventoryUIManager.instance.UpdateInventory();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetItem(InventoryItem item, int slotIndex)
    {
        if (item == null)
        {
            inventoryList[slotIndex] = null;
        }
        else
        {
            inventoryList[item.itemIndex] = null;
            inventoryList[slotIndex] = item;
            item.itemIndex = slotIndex;
        }
    }
}
