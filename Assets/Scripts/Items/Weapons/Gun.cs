using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    public override void UseWeapon()
    {
        Debug.Log("Bang!");
    }
}