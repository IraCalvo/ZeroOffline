using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public int maxHealth;
    public int currentHealth;

    void Awake()
    { 
        
    }

    public void TakeDamage(int dmgToTake, DamageSource dmgSource)
    {
        if (currentHealth > 0)
        {
            if (dmgSource == DamageSource.Enemy)
            {
                currentHealth -= dmgToTake;
                if (currentHealth < 0)
                { 
                    //TODO: Add game lose procedures in game manager
                }
            }
        }
    }
}
