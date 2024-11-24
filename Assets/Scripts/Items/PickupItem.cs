using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            //play pickup sfx
            IItem item = GetComponent<IItem>();
            PlayerInventoryManager.instance.PickupItem(item);
            ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);

            if (InventoryUIManager.instance.inventoryUI.activeInHierarchy)
            {
                InventoryUIManager.instance.UpdateInventory();
            }
        }
    }
}
