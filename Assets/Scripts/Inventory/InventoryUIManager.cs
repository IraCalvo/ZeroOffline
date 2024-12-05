using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    public List<InventoryUISlot> inventoryUISlots = new List<InventoryUISlot>();
    public GameObject inventoryUI;
    public InventoryPickupItem inventoryPickupItem;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else 
        {
            Destroy(instance);
        }
    }

    public void OpenInventory()
    {
        //TODO: play sfx
        inventoryUI.SetActive(true);
        UpdateInventory();
    }

    public void CloseInventory() 
    { 
        //TODO: play sfx
        inventoryUI.SetActive(false);
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < inventoryUISlots.Count; i++)
        {
            InventoryUISlot inventoryUISlot = inventoryUISlots[i];
            InventoryItem inventoryItem = PlayerInventoryManager.instance.inventoryList[i];
            if (inventoryItem != null)
            {
                inventoryUISlot.SetItem(inventoryItem);
            }
        }
    }
}
