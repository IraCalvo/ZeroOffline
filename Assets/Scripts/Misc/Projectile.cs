using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileOwner
{ 
    Player,
    Enemy
}

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float projMS;
    [SerializeField] ProjectileOwner projOwner;
    [SerializeField] int projDMG;
    [SerializeField] float projRange;
    [SerializeField] int projPierceAmount;
    private Vector3 startPos;

    private void OnEnable()
    {
        startPos = transform.position;
    }

    public void UpdateProjMS(float moveSpeed)
    { 
        this.projMS = moveSpeed;
    }

    public void UpdateProjDamage(int dmg)
    { 
        this.projDMG = dmg;
    }

    public void UpdateProjRange(float range)
    {
        this.projRange = range;
    }

    public void UpdatePierceAmount(int pierceAmount)
    {
        this.projPierceAmount = pierceAmount;
    }

    public void UpdateProjOwner(ProjectileOwner proj)
    {
        if (proj == ProjectileOwner.Player)
        {
            projOwner = ProjectileOwner.Player;
        }
        else 
        {
            projOwner = ProjectileOwner.Enemy;
        }
    }

    private void FixedUpdate()
    {
        MoveProjectile();
        DetectProjectileDistance();
    }

    void MoveProjectile()
    {
        //make sure projectile sprites are facing the right
        transform.Translate(Vector3.right * Time.deltaTime * projMS);
    }

    void DetectProjectileDistance()
    {
        if (Vector3.Distance(transform.position, startPos) > projRange)
        { 
            //turn the projectile off in the objectpool
        }
    }
}
