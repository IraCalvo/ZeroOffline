using System;
using TMPro;
using UnityEngine;

public class InventoryItemDescription : MonoBehaviour
{
    public static InventoryItemDescription Instance;
    public InventoryItem currentItem;
    public TextMeshProUGUI descriptionText;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InventoryUISlot.OnPointerEnterEvent += HandlePointerEnter;
        InventoryUISlot.OnPointerExitEvent += HandlePointerExit;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GetMousePositionWithOffset();
    }

    public void HandlePointerEnter(object sender, InventoryUISlot.InventoryUISlotArgs args)
    {
        InventoryItem inventoryItem = args.item;
        descriptionText.gameObject.SetActive(true);

        descriptionText.text = inventoryItem.itemName;
    }

    public void HandlePointerExit(object sender, EventArgs args)
    {
        descriptionText.gameObject.SetActive(false);
    }

    public Vector3 GetMousePositionWithOffset()
    {
        Vector3 mousePosition = Input.mousePosition;
        return mousePosition;
    }
}
