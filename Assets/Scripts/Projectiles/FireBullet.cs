using UnityEngine;

public class FireBullet : Projectile
{
    public int burnDamage;
    public float burnDuration;

    protected override void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag(StringUtils.TagStrings.PlayerTag) && projOwner != DamageOwner.Player
            || otherCollider.CompareTag(StringUtils.TagStrings.EnemyTag) && projOwner != DamageOwner.Enemy)
        {
            IDamageable damagedObject = otherCollider.GetComponent<IDamageable>();
            damagedObject.TakeDamage(projDMG, projOwner, critChance);

            if (projOwner == DamageOwner.Player)
            {

                otherCollider.GetComponent<StatusEffects>().ApplyBurn(burnDamage, burnDuration);

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
