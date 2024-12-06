using UnityEngine;

public class RocketPod : Projectile
{
    [SerializeField] GameObject xplosionToSpawn;
    [SerializeField] int xplosionDMG;

    protected override void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag(StringUtils.TagStrings.PlayerTag) && projOwner != DamageSource.Player
            || otherCollider.CompareTag(StringUtils.TagStrings.EnemyTag) && projOwner != DamageSource.Enemy)
        {
            IDamageable damagedObject = otherCollider.GetComponent<IDamageable>();
            damagedObject.TakeDamage(projDMG, projOwner, critChance);

            if (projOwner == DamageSource.Player)
            {

                GameObject xplosion = ObjectPoolManager.Instance.GetPoolObject(xplosionToSpawn.gameObject);
                xplosion.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
                xplosion.GetComponent<Explosion>().UpdateProjDamage(xplosionDMG);
                xplosion.GetComponent<Explosion>().UpdateCritChance(critChance);

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

            GameObject xplosion = ObjectPoolManager.Instance.GetPoolObject(xplosionToSpawn.gameObject);
            xplosion.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            xplosion.GetComponent<Explosion>().UpdateProjDamage(xplosionDMG);
            xplosion.GetComponent<Explosion>().UpdateCritChance(critChance);
        }
    }
}