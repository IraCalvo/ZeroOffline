using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon, IWeapon
{
    public override void UseWeapon()
    {
        Debug.Log("sword used");
    }
}
