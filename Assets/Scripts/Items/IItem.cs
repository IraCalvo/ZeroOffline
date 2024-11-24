using UnityEngine;

public interface IItem
{
    string ItemName { get; }
    string ItemDesc { get; }
    // Max Stack Amount
    int ItemStackAmount { get; }
    Sprite InventoryIcon { get; }
}
