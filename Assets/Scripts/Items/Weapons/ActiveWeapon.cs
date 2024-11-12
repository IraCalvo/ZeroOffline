using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentWeapon;
    [SerializeField] int currentAmmo;
    int maxAmmo;
    float reloadTime;
    public bool isReloading = false;

    private void Start()
    {
        NewWeapon(currentWeapon);
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        currentWeapon = newWeapon;
        maxAmmo = currentWeapon.GetComponent<Weapon>()._weaponSO.maxAmmoAmount;
        currentAmmo = maxAmmo;
        reloadTime = currentWeapon.GetComponent<Weapon>()._weaponSO.baseReloadTime;
    }

    public void Attack()
    {
        //TODO: type check, make sure current weapon is a weapon
        if (currentAmmo > 0 && !isReloading)
        {
            (currentWeapon as IWeapon).UseWeapon();
            currentAmmo--;
        }
        else if(!isReloading)
        {
            StartCoroutine(ReloadCoroutine());
            ReloadBar.instance.ShowReloadProgress(reloadTime);
        }
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
    }
}
