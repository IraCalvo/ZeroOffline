using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : Weapon
{
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile projectileToShoot;

    public override void UseWeapon()
    {
        
    }
}
