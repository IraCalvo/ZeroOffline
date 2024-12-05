using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPickupItem : MonoBehaviour
{

    public InventoryItem item;
    public Image itemImage;
    public TextMeshProUGUI itemAmount;

    private void Awake()
    {
    }

    public void SetItem(InventoryItem item)
    {
        this.item = item;
        
        if (item == null)
        {
            DeactivatePickupItem();
        }
        else
        {
            transform.position = GetMousePosition();
            ActivatePickupItem();
        }
    }

    public void DeactivatePickupItem()
    {
        gameObject.SetActive(false);
        itemImage.gameObject.SetActive(false);
        itemAmount.gameObject.SetActive(false);
    }

    public void ActivatePickupItem()
    {
        gameObject.SetActive(true);
        itemImage.gameObject.SetActive(true);
        itemAmount.gameObject.SetActive(true);
        itemImage.sprite = item.itemSpriteIcon;
        itemAmount.text = item.currentAmount.ToString();
    }

    public void Update()
    {
        transform.position = GetMousePosition();
    }

    public Vector3 GetMousePosition()
    {
        return Input.mousePosition;
    }

}
