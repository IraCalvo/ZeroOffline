using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Weapon
{
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile projectileToShoot;

    public override void UseWeapon()
    {
        float targetAngle = Mathf.Atan2(CursorManager.instance.aimDir.y, CursorManager.instance.aimDir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        GameObject bullet = ObjectPoolManager.Instance.GetPoolObject(projectileToShoot.gameObject);
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = targetRotation;
        bullet.GetComponent<Projectile>().UpdateProjMS(_weaponSO.projectileMS);
        PlayerController.instance.SetAttackCD(_weaponSO.weaponCDBase);
    }
}
