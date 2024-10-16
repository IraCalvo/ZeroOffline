using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public abstract class BaseShooter : Enemy, IEnemy 
{
    [SerializeField] Projectile projectileToShoot;
    [SerializeField] private float projMoveSpeed;
    [SerializeField] private float burstCount;
    [SerializeField] private int projPerBurst;
    [SerializeField] [Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool staggerBullets;
    [SerializeField] private bool oscillate;

    private bool isShooting = false;

    private void OnValidate()
    {
        if (oscillate) { staggerBullets = true; }
        if (oscillate == false) { staggerBullets = false; }
        if (projPerBurst < 1) { projPerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (timeBetweenBursts < 0.1f) { timeBetweenBursts = 0.1f; }
        if (restTime < 0.1f) { restTime = 0.1f; }
        if (startingDistance < 0.1f) { restTime = 0.1f; }
        if (angleSpread == 0) { projPerBurst = 1; }
        if (projMoveSpeed <= 0) { projMoveSpeed = 0.1f; }
    }

    public void FixedUpdate()
    {
        ManageEnemyState();
    }



    public void ManageEnemyState()
    {
        switch (enemyState)
        {
            case EnemyState.Spawning:
                CheckIfDoneSpawning();
                break;
            case EnemyState.Moving:
                MoveToRange();
                break;
            case EnemyState.Attacking:
                Attack();
                break;
            case EnemyState.Death:
                break;
        }
    }

    public void CheckIfDoneSpawning()
    {
        enemyState = EnemyState.Moving;
    }

    public void MoveToRange()
    {

        if (CheckIfInAttackRange())
        {
            enemyState = EnemyState.Attacking;
        }
        else
        {
            enemyState = EnemyState.Moving;

            transform.position = Vector2.MoveTowards(transform.position, PlayerController.instance.transform.position, currentEnemyMS * Time.deltaTime);
        }
    }

    bool CheckIfInAttackRange()
    {
        if (Vector2.Distance(transform.position, PlayerController.instance.transform.position) < currentAttackRange)
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    public void Attack()
    {
        if (CheckIfInAttackRange())
        {
            if (isShooting == false)
            {
                StartCoroutine(ShootRoutine());
            }
        }
        else 
        {
            enemyState = EnemyState.Moving;
        }
    }

    private IEnumerator ShootRoutine()
    {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        GetTargetToShootAt(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (staggerBullets)
        {
            timeBetweenProjectiles = timeBetweenBursts / projPerBurst;
        }

        for (int i = 0; i < burstCount; i++)
        {
            if (!oscillate)
            {
                GetTargetToShootAt(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1)
            {
                GetTargetToShootAt(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if(oscillate)
            {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            GetTargetToShootAt(out startAngle, out currentAngle, out angleStep, out endAngle);
            for (int j = 0; j < projPerBurst; j++)
            {
                Vector2 projSpawnPos = FindProjSpawnPos(currentAngle);

                GameObject bullet = ObjectPoolManager.Instance.GetPoolObject(projectileToShoot.gameObject);
                bullet.transform.position = projSpawnPos;
                bullet.transform.right = bullet.transform.position - transform.position;

                if (bullet.TryGetComponent(out Projectile proj))
                {
                    proj.UpdateProjMS(projMoveSpeed);
                    proj.UpdateProjOwner(ProjectileOwner.Enemy);
                }

                currentAngle += angleStep;
                if (staggerBullets)
                {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }

            }

            //reset angle for the next burst
            currentAngle = startAngle;


            if (staggerBullets == false)
            {
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void GetTargetToShootAt(out float startAngle, out float currentAngle, out float angleStep, out float endAngle)
    {
        Vector2 targetDirection = PlayerController.instance.transform.position - transform.position;

        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;

        //this means it can just shoot in a straight line at the player if it is 0
        if (angleSpread != 0)
        {
            angleStep = angleSpread / (projPerBurst - 1);
            halfAngleSpread = angleSpread / 2f;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindProjSpawnPos(float currentAngle)
    {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
