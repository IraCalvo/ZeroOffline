using UnityEngine;

public class OneBullet : MonoBehaviour
{
    public int damageToAdd;

    public void ApplyEffect()
    {
        //TODO: should find a way to prevent them gaining more ammo for when they switch weapons
        ActiveWeapon.Instance.maxAmmo = 1;
        PlayerStats.instance.Damage.Add(damageToAdd);
    }
}
