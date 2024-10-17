using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    Enemy enemy;
    int currentEnemyHealth;

    void OnEnable()
    { 
        enemy = GetComponent<Enemy>();
        currentEnemyHealth = enemy.enemySO.enemyBaseMaxHP;
    }

    public virtual void TakeDamage(int dmgToTake, DamageSource dmgSource)
    {
        if (dmgSource == DamageSource.Player)
        {
            currentEnemyHealth -= dmgToTake;
            //TODO: Make it flash white when take damage and show text of how much damage they take
            if (currentEnemyHealth <= 0)
            {
                ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
            }
        }

    }
}
