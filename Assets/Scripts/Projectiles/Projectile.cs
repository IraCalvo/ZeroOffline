using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageSource
{ 
    Player,
    Enemy,
    Neutral
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float projMS;
    [SerializeField] public DamageSource projOwner;
    [SerializeField] public int projDMG;
    [SerializeField] public float critChance;
    [SerializeField] float projRange;
    [SerializeField] public int projPierceAmount;
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

    public virtual void UpdateCritChance(float critPercent)
    { 
        this.critChance = critPercent;
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
            //ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag(StringUtils.TagStrings.PlayerTag) && projOwner != DamageSource.Player
            || otherCollider.CompareTag(StringUtils.TagStrings.EnemyTag) && projOwner != DamageSource.Enemy)
        {
            IDamageable damagedObject = otherCollider.GetComponent<IDamageable>();
            damagedObject.TakeDamage(projDMG, projOwner, critChance);

            if (projOwner == DamageSource.Player)
            {
                projPierceAmount--;
                if (projPierceAmount <= 0)
                {
                    ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
                }
            }
        }
        else if (otherCollider.gameObject.layer == LayerMask.NameToLayer(StringUtils.TagStrings.WallTag) ||
            otherCollider.gameObject.layer == LayerMask.NameToLayer(StringUtils.TagStrings.ObstactleTag))
        {
            ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
        }
    }
}
