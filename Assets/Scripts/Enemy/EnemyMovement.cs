using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyAIBrain enemyAIBrain;
    Enemy enemy;
    Vector2 enemyMoveDir;
    StatusEffects statusEffects;
    IEnemy enemyAttackScript;
    [SerializeField] int idleWaitMin;
    [SerializeField] int idleWaitMax;
    bool coroutineRunning;
    bool movingToNextIdle;

    private void Awake()
    {
        enemyAIBrain = GetComponent<EnemyAIBrain>();
        enemy = GetComponent<Enemy>();
        statusEffects = GetComponent<StatusEffects>();
        enemyAttackScript = GetComponent<IEnemy>();
    }

    private void Update()
    {
        if (statusEffects.isStunned == false)
        {
            switch (enemy.enemyState)
            {
                case EnemyState.Idle:
                    EnemyIdle();
                    break;
                case EnemyState.Chasing:
                    ChasePlayer();
                    break;
                case EnemyState.Attacking:
                    CheckAttackRange();
                    enemyAttackScript.Attack();
                    break;
                case EnemyState.Death:
                    break;
            }
        }
    }

    void EnemyIdle()
    {
        if (coroutineRunning == false || movingToNextIdle == false)
        {
            StartCoroutine(WaitUntilNextLocation());
        }
    }

    IEnumerator WaitUntilNextLocation()
    {
        coroutineRunning = true;
        int randNumm = FunctionUtils.RandomChance(idleWaitMin, idleWaitMax);
        yield return new WaitForSeconds(randNumm);
        ChooseRandomLocation();

    }

    void ChooseRandomLocation()
    {
        //TODO: move enemy, and make sure its a valid spot as well.
    }

    void ChasePlayer()
    {
        if (enemyAIBrain.currentTarget != null)
        {
            float distance = Vector2.Distance(enemyAIBrain.currentTarget.position, transform.position);
            if (distance > enemy.enemySO.enemyAttackRange)
            {
                enemyMoveDir = enemyAIBrain.GetDirectionToMove();
                transform.position += (enemy.enemySO.enemyBaseMS * Time.deltaTime * (Vector3)enemyMoveDir) * statusEffects.slowAmount;
            }
            else
            {
                enemy.enemyState = EnemyState.Attacking;
            }
        }
        else if (enemyAIBrain.GetTargetsCount() > 0)
        {
            enemyAIBrain.currentTarget = enemyAIBrain.targets[0];
        }
    }

    void CheckAttackRange()
    {
        float distance = Vector2.Distance(enemyAIBrain.currentTarget.position, transform.position);
        if (distance <= enemy.enemySO.enemyAttackRange)
        {
            enemy.enemyState = EnemyState.Attacking;
        }
        else
        {
            enemy.enemyState = EnemyState.Chasing;
        }
    }
}
