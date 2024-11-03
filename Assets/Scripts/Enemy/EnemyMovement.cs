using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyAIBrain enemyBrain;

    //Variables for steering
    [SerializeField] private float radius = 2f;
    [SerializeField] private float agentColliderSize = 0.6f;
    [SerializeField] bool showGizmo = true;
    float[] dangersResultTemp = null;

    //variables for seeking
    [SerializeField] private float targetReachedThreshold = 0.5f;
    [SerializeField] bool showSeekingGizmo;
    bool reachedLastTarget = true;
    private Vector2 targetPosCached;
    private float[] interestsTemp;

    private void Awake()
    {
        enemyBrain = GetComponent<EnemyAIBrain>();
    }

    public (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest)
    {
        foreach (Collider2D obstacleCollider in enemyBrain.obstacles)
        {
            Vector2 dirToObstacle = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = dirToObstacle.magnitude;

            float weight = distanceToObstacle <= agentColliderSize ? 1 : (radius - distanceToObstacle) / radius;

            Vector2 directionToObstacleNormalized = dirToObstacle.normalized;

            for (int i = 0; i < Directions.eightDirections.Count; i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);
                float valueToPutIn = result * weight;

                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }
        }

        dangersResultTemp = danger;
        return(danger, interest);
    }

    public (float[] danger, float[] interest) GetSeeking(float[] danger, float[] interest)
    {
        if (reachedLastTarget)
        {
            if (enemyBrain.targets == null || enemyBrain.targets.Count <= 0)
            {
                enemyBrain.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                enemyBrain.currentTarget = enemyBrain.targets.OrderBy(target => Vector2.Distance(target.position, transform.position)).FirstOrDefault();
            }
        }

        if (enemyBrain.currentTarget != null && enemyBrain.targets != null && enemyBrain.targets.Contains(enemyBrain.currentTarget))
        { 
            targetPosCached = enemyBrain.currentTarget.position;
        }

        if (Vector2.Distance(transform.position, targetPosCached) < targetReachedThreshold)
        {
            reachedLastTarget = true;
            enemyBrain.currentTarget = null;
            return (danger, interest);
        }

        Vector2 dirToTar = (targetPosCached - (Vector2)transform.position);
        for (int i = 0; i < interest.Length; i++) 
        {
            float result = Vector2.Dot(dirToTar.normalized, Directions.eightDirections[i]);

            if (result > 0)
            {
                float valueToPutIn = result;
                if (valueToPutIn > interest[i])
                {
                    interest[i] = valueToPutIn;
                }
            }
        }

        interestsTemp = interest;
        return (danger, interestsTemp);
    }

    private void OnDrawGizmos()
    {
        if (showSeekingGizmo == false)
        {
            return;
        }

        Gizmos.DrawSphere(targetPosCached, 0.2f);
        if (Application.isPlaying && interestsTemp != null)
        {
            if (interestsTemp != null)
            {
                Gizmos.color = Color.green;
                for (int i = 0; i < interestsTemp.Length; i++)
                {
                    Gizmos.DrawRay(transform.position, Directions.eightDirections[i] * interestsTemp[i]);
                }
                if (reachedLastTarget == false)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(targetPosCached, 0.1f);
                }
            }
        }
    }
}

public static class Directions
{
    public static List<Vector2> eightDirections = new List<Vector2>
    {
        new Vector2(0, 1).normalized,
        new Vector2(1, 1).normalized,
        new Vector2(1, 0).normalized,
        new Vector2(1, -1).normalized,
        new Vector2(-1, -1).normalized,
        new Vector2(0, -1).normalized,
        new Vector2(-1, 0).normalized,
        new Vector2(-1, 1).normalized
    };
}
