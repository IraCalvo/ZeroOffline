using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyAIBrain enemyAIBrain;
    Enemy enemy;
    Vector2 enemyMoveDir;

    private void Awake()
    {
        enemyAIBrain = GetComponent<EnemyAIBrain>();
        enemy = GetComponent<Enemy>();
    }

    private void Update()
    {
        if (enemyAIBrain.currentTarget != null)
        {
            //Debug.Log("current target not null");
            float distance = Vector2.Distance(enemyAIBrain.currentTarget.position, transform.position);
            if (distance > enemy.enemySO.enemyAttackRange)
            {
                enemyMoveDir = enemyAIBrain.GetDirectionToMove();
                transform.position += enemy.enemySO.enemyBaseMS * Time.deltaTime * (Vector3)enemyMoveDir;
            }
            else 
            {
                //do attack stuff, can add delay to check so that its not ran every frame
            }
        }
        else if (enemyAIBrain.GetTargetsCount() > 0)
        {
            enemyAIBrain.currentTarget = enemyAIBrain.targets[0];
        }
    }
}
