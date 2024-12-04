using UnityEngine;

public class BasicPod : Pod
{
    [SerializeField] Transform firePoint;
    [SerializeField] Projectile projectileToShoot;

    public override  void UsePod()
    {
        float targetAngle = Mathf.Atan2(CursorManager.instance.aimDir.y, CursorManager.instance.aimDir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);

        GameObject pod = ObjectPoolManager.Instance.GetPoolObject(projectileToShoot.gameObject);
        pod.transform.SetPositionAndRotation(firePoint.position, targetRotation);
        pod.GetComponent<Projectile>().UpdateProjMS(_podSO.podProjectileMS);
        pod.GetComponent<Projectile>().UpdateCritChance(_podSO.podBaseCritChance);
        pod.GetComponent<Projectile>().UpdateProjDamage(_podSO.podBaseDMG);
    }

}
