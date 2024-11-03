using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EnemyState
{
    Spawning,
    Idle,
    Chasing,
    Attacking,
    Death
}

public class Enemy : MonoBehaviour, IDamageable
{
    public EnemySO enemySO;
    public float currentHP;
    public float currentEnemyMS;
    public float currentAttackRange;
    public EnemyState enemyState;
    public TextMeshProUGUI dmgTakenText;

    private void OnEnable()
    {
        currentHP = enemySO.enemyBaseMaxHP;
        currentEnemyMS = enemySO.enemyBaseMS;
        currentAttackRange = enemySO.enemyBaseRange;
    }

    public void TakeDamage(int damageToTake, DamageSource dmgSource)
    {
        if (dmgSource == DamageSource.Player || dmgSource == DamageSource.Neutral)
        {
            currentHP -= damageToTake;
            if (currentHP <= 0)
            {
                DeathProcedures();
            }
        }
    }

    void DeathProcedures()
    {
        ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
    }
}
