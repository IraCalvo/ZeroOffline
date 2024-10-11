using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Spawning,
    Moving,
    Attacking,
    Death
}

public abstract class Enemy : MonoBehaviour
{
    public EnemySO enemySO;
    public float currentHP;
    public float currentEnemyMS;
    public float currentAttackRange;
    public EnemyState enemyState;

    private void OnEnable()
    {
        currentHP = enemySO.enemyBaseMaxHP;
        currentEnemyMS = enemySO.enemyBaseMS;
        currentAttackRange = enemySO.enemyBaseRange;
    }

}
