using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageSource
{ 
    Player,
    Enemy
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float projMS;
    [SerializeField] DamageSource projOwner;
    [SerializeField] int projDMG;
    [SerializeField] float projRange;
    [SerializeField] int projPierceAmount;
    private Vector3 startPos;

    public virtual void OnEnable()
    {
        startPos = transform.position;
    }

    public virtual void UpdateProjMS(float moveSpeed)
    { 
        this.projMS = moveSpeed;
    }

    public virtual void UpdateProjDamage(int dmg)
    { 
        this.projDMG = dmg;
    }

    public virtual void UpdateProjRange(float range)
    {
        this.projRange = range;
    }

    public virtual void UpdatePierceAmount(int pierceAmount)
    {
        this.projPierceAmount = pierceAmount;
    }

    public virtual void UpdateProjOwner(DamageSource proj)
    {
        if (proj == DamageSource.Player)
        {
            projOwner = DamageSource.Player;
        }
        else 
        {
            projOwner = DamageSource.Enemy;
        }
    }

    public virtual void FixedUpdate()
    {
        MoveProjectile();
        DetectProjectileDistance();
    }

    public virtual void MoveProjectile()
    {
        //make sure projectile sprites are facing the right
        transform.Translate(Vector3.right * Time.deltaTime * projMS);
    }

    public virtual void DetectProjectileDistance()
    {
        if (Vector3.Distance(transform.position, startPos) > projRange)
        { 
            //turn the projectile off in the objectpool
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player") || otherCollider.CompareTag("Enemy"))
        {
            otherCollider.GetComponent<IDamageable>().TakeDamage(projDMG, projOwner);
        }

        //object layer
        if (otherCollider.gameObject.layer == 6)
        {
            ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
        }
    }
}
