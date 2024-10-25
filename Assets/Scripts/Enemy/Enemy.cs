using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum EnemyState
{
    Spawning,
    Moving,
    Attacking,
    Death
}

public class Enemy : MonoBehaviour
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

}
