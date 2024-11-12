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
        bullet.transform.SetPositionAndRotation(firePoint.position, targetRotation);
        bullet.GetComponent<Projectile>().UpdateProjMS(_weaponSO.projectileMS);
        bullet.GetComponent<Projectile>().UpdateCritChance(_weaponSO.weaponBaseCritChance);
        bullet.GetComponent<Projectile>().UpdateProjDamage(_weaponSO.weaponDamageBase);
        PlayerController.instance.SetAttackCD(_weaponSO.weaponCDBase);
    }
}
