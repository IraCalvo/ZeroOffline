using UnityEngine;

public abstract class Pod : MonoBehaviour, IItem
{
    [SerializeField] string _itemName;
    [SerializeField] string _itemDesc;
    int _itemStackAmount = 1;
    [SerializeField] public PodSO _podSO;
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

    public PodSO PodSO
    {
        get { return _podSO; }
        set { _podSO = value; }
    }

    public Sprite InventoryIcon
    {
        get { return _inventoryIcon; }
        set { _inventoryIcon = value; }
    }

    public abstract void UsePod();
}
