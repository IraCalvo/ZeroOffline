using System.Collections;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public bool isOnFire;
    public bool isStunned;

    float elapsedBurnTime;

    public void ApplyBurn(int burnDamage, float burnDuration)
    {
        if (isOnFire == false)
        {
            isOnFire = true;
            elapsedBurnTime = 0;
            StartCoroutine(BurnStatusCoroutine(burnDamage, burnDuration));
        }
        else
        {
            elapsedBurnTime = 0f;
        }
    }

    IEnumerator BurnStatusCoroutine(int burnDamage, float burnDuration)
    {
        float burnTickTimer = 0f;
        while (elapsedBurnTime < burnDuration)
        {
            burnTickTimer += Time.deltaTime;
            if (burnTickTimer >= IntUtils.DefaultDebufAndBuffTimers.DefaultBurnInterval)
            {
                gameObject.GetComponent<IDamageable>().TakeDamage(burnDamage, DamageSource.Neutral, 0);
                burnTickTimer = 0f;
            }

            elapsedBurnTime += Time.deltaTime;
            yield return null;
        }

        isOnFire = false;
    }
}
