using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyAIBrain : MonoBehaviour
{
    Enemy enemy;
    EnemySO enemySO;
    EnemySteeringMovement enemySteeringMovement;

    //variables needed for AI movement
    public List<Transform> targets = null;
    public Collider2D[] obstacles = null;
    public Transform currentTarget;
    [SerializeField] private float detectionDelay;
    Vector2 resultDir = Vector2.zero;

    //variables for Detection of obstacles and terrain
    [SerializeField] private float detectionRadius = 2;
    [SerializeField] private LayerMask layermask;
    [SerializeField] bool showGizmos = true;
    Collider2D[] colliders;

    //variables for detection of target
    [SerializeField] private float targetDetectionRange = 5;
    [SerializeField] private LayerMask obstacleLayerMask;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private bool showTargetDetectionGizmos = false;
    [SerializeField] private List<Transform> targetColliders;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        enemySO = enemy.enemySO;
        enemySteeringMovement = GetComponent<EnemySteeringMovement>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(PerformDetection), 0, detectionDelay);
    }

    private void PerformDetection()
    {
        Detect();
        DetectPlayer();
    }

    //Enemy pathfinding and moving
    public int GetTargetsCount() => targets == null ? 0 : targets.Count;

    public void Detect()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius, layermask);
        obstacles = colliders;
    }

    public void DetectPlayer()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);

        if (playerCollider != null)
        {
            Vector2 dir = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, targetDetectionRange, obstacleLayerMask);

            if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
            {
                targetColliders = new List<Transform>() { playerCollider.transform };
            }
            else
            {
                targetColliders = null;
            }
        }
        else
        {
            targetColliders = null;
        }
        targets = targetColliders;
    }

    public Vector2 GetDirectionToMove()
    {
        float[] danger = new float[8];
        float[] interest = new float[8];

        enemySteeringMovement.GetSteering(danger, interest);
        enemySteeringMovement.GetSeeking(danger, interest);

        //subtract danger values fro interest array
        for (int i = 0; i < 8; i++)
        {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }

        Vector2 outputDir = Vector2.zero;
        for (int i = 0; i < 8; i++)
        {
            outputDir += Directions.eightDirections[i] * interest[i];
        }
        outputDir.Normalize();
        resultDir = outputDir;

        return resultDir;
    }

    private void OnDrawGizmos()
    {
        if (showGizmos == false)
        {
            return;
        }
        if (Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D obstacleCollider in colliders)
            {
                Gizmos.DrawSphere(obstacleCollider.transform.position, 0.2f);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (showTargetDetectionGizmos == false)
        {
            return;
        }

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (targetColliders == null)
        {
            return;
        }

        Gizmos.color = Color.blue;
        foreach (var item in targetColliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
