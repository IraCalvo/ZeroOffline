using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour, IWeapon
{
    [SerializeField] string _itemName;
    [SerializeField] string _itemDesc;
    [SerializeField] public WeaponSO _weaponSO;
    int _itemStackAmount = 1;
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
        set {_itemStackAmount = value; }
    }

    public WeaponSO WeaponSO
    {
        get { return _weaponSO; }
        set { _weaponSO = value; }
    }

    public abstract void UseWeapon();
}
