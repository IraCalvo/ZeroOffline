using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager instance;
    public List<InventoryUISlot> inventoryUISlots = new List<InventoryUISlot>();
    public GameObject inventoryUI;

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
        //play sfx
        inventoryUI.SetActive(true);
        UpdateInventory();
    }

    public void CloseInventory() 
    { 
        //play sfx
        inventoryUI.SetActive(false);
    }

    public void UpdateInventory()
    {
        for (int i = 0; i < inventoryUISlots.Count; i++)
        {
            if (PlayerInventoryManager.instance.inventoryList[i] != null)
            {
                inventoryUISlots[i].itemAmount.text = PlayerInventoryManager.instance.inventoryList[i].currentAmount.ToString();
                inventoryUISlots[i].itemSprite.sprite = PlayerInventoryManager.instance.inventoryList[i].itemSpriteIcon;

                //change the alpha back to max
                Color inventorySpriteAlpha = inventoryUISlots[i].itemSprite.color;
                inventorySpriteAlpha.a = 1f;
                inventoryUISlots[i].itemSprite.color = inventorySpriteAlpha;
            }
        }
    }
}
