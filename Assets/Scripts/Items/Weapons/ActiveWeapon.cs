using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentWeapon;
    WeaponSO activeWeaponSO;
    [SerializeField] int currentAmmo;
    int maxAmmo;
    float reloadTime;
    public bool isReloading = false;
    public bool canAttack = true;

    private void Start()
    {
        NewWeapon(currentWeapon);
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        currentWeapon = newWeapon;
        activeWeaponSO = newWeapon.GetComponent<IWeapon>().WeaponSO;
        maxAmmo = activeWeaponSO.maxAmmoAmount;
        currentAmmo = maxAmmo;
        reloadTime = activeWeaponSO.baseReloadTime;
    }

    public void Attack()
    {
        if (canAttack)
        {
            //TODO: type check, make sure current weapon is a weapon
            if (currentAmmo > 0 && !isReloading)
            {
                (currentWeapon as IWeapon).UseWeapon();
                currentAmmo--;
                PlayerUIManager.instance.UpdateAmmoCount(currentAmmo);
                StartCoroutine(AttackCDCoroutine(activeWeaponSO.weaponCDBase));
            }
            else if (!isReloading)
            {
                StartCoroutine(ReloadCoroutine());
                ReloadBar.instance.ShowReloadProgress(reloadTime);
            }
        }
    }

    IEnumerator AttackCDCoroutine(float cd)
    {
        canAttack = false;
        yield return new WaitForSeconds(cd);
        canAttack = true;
    }

    public void Reload()
    {
        if (currentAmmo != maxAmmo && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
            ReloadBar.instance.ShowReloadProgress(reloadTime);
        }
    }

    IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        PlayerUIManager.instance.UpdateAmmoCount(currentAmmo);
    }
}
