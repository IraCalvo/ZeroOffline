using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    [SerializeField] private MonoBehaviour currentWeapon;

    public void Attack()
    {
        //TODO: weapon CD
        //TODO: type check, make sure current weapon is a weapon
        (currentWeapon as IWeapon).UseWeapon();
    }
}
