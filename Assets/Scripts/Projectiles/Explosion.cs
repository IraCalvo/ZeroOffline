using UnityEngine;

public class Explosion : Projectile
{
    public void OnEnable()
    {
        Animator animator = GetComponent<Animator>();
        animator.SetTrigger(StringUtils.AnimStrings.StartAnimation);
    }

    public void FinishAnimEvent()
    { 
        ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
    }
}
