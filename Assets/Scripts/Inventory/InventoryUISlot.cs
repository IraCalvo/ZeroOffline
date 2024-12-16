using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class InventoryUISlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI itemAmount;
    public Image itemSprite;
    public InventoryItem item;
    public int slotIndex;

    public static event EventHandler<InventoryUISlotArgs> OnPointerEnterEvent;
    public static event EventHandler OnPointerExitEvent;
    public class InventoryUISlotArgs : EventArgs
    {
        public InventoryItem item;
    }


    private void Awake()
    {
        UpdateItem();
    }

    private void Start()
    {
        UpdateItem();
    }

    public void SetItem(InventoryItem inventoryItem)
    {
        this.item = inventoryItem;
        PlayerInventoryManager.instance.SetItem(inventoryItem, slotIndex);
        UpdateItem();
    }

    public void UpdateItem()
    {
        if (item == null || item.itemName == "")
        {
            itemAmount.text = "";
            itemSprite.sprite = null;

            // Make Sprite invisible
            Color inventorySpriteAlpha = itemSprite.color;
            inventorySpriteAlpha.a = 0f;
            itemSprite.color = inventorySpriteAlpha;
        }
        else
        {
            itemAmount.text = item.currentAmount.ToString();
            itemSprite.sprite = item.itemSpriteIcon;

            // Make Sprite Visible
            Color inventorySpriteAlpha = itemSprite.color;
            inventorySpriteAlpha.a = 1f;
            itemSprite.color = inventorySpriteAlpha;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryPickupItem inventoryPickupItem = InventoryUIManager.instance.inventoryPickupItem;
        
        if (this.item != null && item.itemName != "" && !inventoryPickupItem.isActiveAndEnabled)
        {
            OnPointerEnterEvent?.Invoke(this, new InventoryUISlotArgs { item = item });
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (this.item != null && item.itemName != "")
        {
            OnPointerExitEvent?.Invoke(this, new EventArgs());
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnPointerLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnPointerRightClick();
        }
    }

    public void OnPointerLeftClick()
    {
        InventoryPickupItem inventoryPickupItem = InventoryUIManager.instance.inventoryPickupItem;
        // There is no item currently picked up
        if (!inventoryPickupItem.isActiveAndEnabled)
        {
            if (item != null && item.itemName != "")
            {
                inventoryPickupItem.SetItem(item);
                SetItem(null);
            }
        }
        // There is an item currently being held
        else if (inventoryPickupItem.isActiveAndEnabled)
        {
            // There is no item in this slot
            if (item == null || item.itemName == "")
            {
                SetItem(inventoryPickupItem.item);
                inventoryPickupItem.DeactivatePickupItem();
            }
            else
            {
            // There is an item in this slot. Check if it is the same item and if we can stack them together. Otherwise swap
                if (item.itemName == inventoryPickupItem.item.itemName)
                {
                    // We can add the full stack into this slot
                    if (item.currentAmount + inventoryPickupItem.item.currentAmount <= item.itemMaxStackAmount)
                    {
                        InventoryItem pickupItem = new InventoryItem(item.itemName, item.itemDesc, item.itemMaxStackAmount, item.currentAmount + inventoryPickupItem.item.currentAmount, item.itemSpriteIcon);
                        SetItem(pickupItem);
                        inventoryPickupItem.DeactivatePickupItem();
                    }
                    // We can't add the full stack, so just add as many as we can to max it out
                    else
                    {
                        int amountToAdd = item.itemMaxStackAmount - item.currentAmount;
                        inventoryPickupItem.item.currentAmount -= amountToAdd;

                        InventoryItem pickupItem = new InventoryItem(item.itemName, item.itemDesc, item.itemMaxStackAmount, item.itemMaxStackAmount, item.itemSpriteIcon);
                        SetItem(pickupItem);
                        inventoryPickupItem.SetItem(inventoryPickupItem.item);

                    }
                }
                else
                {
                    InventoryItem swapItem = item;
                    SetItem(inventoryPickupItem.item);
                    inventoryPickupItem.SetItem(swapItem);
                }
                
            }
        }
    }

    public void OnPointerRightClick()
    {
        InventoryPickupItem inventoryPickupItem = InventoryUIManager.instance.inventoryPickupItem;
        // There is no item currently picked up
        if (!inventoryPickupItem.isActiveAndEnabled)
        {
            if (item != null && item.itemName != "")
            {
                // If only one item in stack, just pick up the whole stack
                if (item.currentAmount == 1)
                {
                    inventoryPickupItem.SetItem(item);
                    SetItem(null);
                }
                else
                {
                    // Pickup half of the item stack
                    int amountToPickup = 0;
                    if (item.currentAmount % 2 == 0)
                    {
                        amountToPickup = item.currentAmount / 2;
                    }
                    else
                    {
                        amountToPickup = (item.currentAmount / 2) + 1;
                    }

                    item.currentAmount = item.currentAmount - amountToPickup;
                    InventoryItem pickupItem = new InventoryItem(item.itemName, item.itemDesc, item.itemMaxStackAmount, amountToPickup, item.itemSpriteIcon);
                    inventoryPickupItem.SetItem(pickupItem);
                    SetItem(item);
                }
            }
        }
        // There is an item currently being held
        else if (inventoryPickupItem.isActiveAndEnabled)
        {
            // There is no item in this slot
            if (item == null || item.itemName == "")
            {
                // Last item currently being held
                if (inventoryPickupItem.item.currentAmount == 1)
                {
                    SetItem(inventoryPickupItem.item);
                    inventoryPickupItem.DeactivatePickupItem();
                }
                else
                {
                    // Only drop one
                    InventoryItem droppedItem = new InventoryItem(inventoryPickupItem.item.itemName, inventoryPickupItem.item.itemDesc, inventoryPickupItem.item.itemMaxStackAmount, 1, inventoryPickupItem.item.itemSpriteIcon);
                    SetItem(droppedItem);
                    inventoryPickupItem.item.currentAmount--;
                    if (inventoryPickupItem.item.currentAmount == 0)
                    {
                        inventoryPickupItem.DeactivatePickupItem();
                    }
                    else
                    {
                        inventoryPickupItem.SetItem(inventoryPickupItem.item);
                    }
                }
            }
            else
            {
                // There's an item in this slot. Check if it is the same item or not and if we can drop an item here
                if (item.itemName == inventoryPickupItem.item.itemName &&
                    item.currentAmount < item.itemMaxStackAmount)
                {
                    InventoryItem droppedItem = new InventoryItem(inventoryPickupItem.item.itemName, inventoryPickupItem.item.itemDesc, inventoryPickupItem.item.itemMaxStackAmount, item.currentAmount + 1, inventoryPickupItem.item.itemSpriteIcon);
                    SetItem(droppedItem);
                    inventoryPickupItem.item.currentAmount--;
                    if (inventoryPickupItem.item.currentAmount == 0)
                    {
                        inventoryPickupItem.DeactivatePickupItem();
                    }
                    else
                    {
                        inventoryPickupItem.SetItem(inventoryPickupItem.item);
                    }
                }
                else
                {
                    // Either the item's are not the same or it is full here. So we can just swap items here
                    InventoryItem swapItem = item;
                    SetItem(inventoryPickupItem.item);
                    inventoryPickupItem.SetItem(swapItem);
                }
            }
        }
    }
}
