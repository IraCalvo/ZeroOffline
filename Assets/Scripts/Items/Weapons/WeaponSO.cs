using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[CreateAssetMenu(menuName = "WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public Weapon weapon;
    public GameObject weaponPrefab;
    public float weaponCDBase;
    public float weaponDamageBase;
    public float weaponKnockbackBase;
    public float projectileMS;
    public WeaponRarity weaponRarityBase;
}
