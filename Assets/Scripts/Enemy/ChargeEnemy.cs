using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ChargeEnemy : MonoBehaviour
{
    EnemyAIBrain enemyAIBrain;
    Enemy enemy;
    [SerializeField] float chargeAttackCD;
    [SerializeField] float chargeSpeed;
    [SerializeField] float chargeDuration;
    [SerializeField] float windupTime;
    bool isCharging = false;
    bool canAttack = true;

    Rigidbody2D rb;

    void Awake()
    { 
        enemyAIBrain = GetComponent<EnemyAIBrain>();
        enemy = GetComponent<Enemy>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (enemy.enemyState == EnemyState.Attacking)
        {
            ChargeAtPlayer();
        }
    }

    void ChargeAtPlayer()
    {
        if (!isCharging && canAttack)
        {
            StartCoroutine(ChargingAtPlayerCoroutine());
        }
    }

    IEnumerator ChargingAtPlayerCoroutine()
    { 
        isCharging = true;

        Vector2 dir = (enemyAIBrain.currentTarget.position - transform.position).normalized;
        rb.linearVelocity = dir * chargeSpeed;

        yield return new WaitForSeconds(chargeDuration);

        rb.linearVelocity = Vector2.zero;
        isCharging = false;
        canAttack = false;
        enemy.enemyState = EnemyState.Chasing;
        StartCoroutine(AttackCDCoroutine());
    }

    IEnumerator AttackCDCoroutine()
    {
        yield return new WaitForSeconds(chargeAttackCD);
        canAttack = true;
    }
}
