using System.Collections;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public bool isOnFire;
    public bool isStunned;
    public bool isSlowed;

    public float slowAmount = 1f;

    float elapsedBurnTime;
    float elapsedStunTime;
    float elapsedSlowTime;

    IDamageable healthScript;

    private void Awake()
    {
        healthScript = GetComponent<IDamageable>();
    }

    public void ApplyStun(float stunDuration)
    {
        if (isStunned == false)
        {
            StartCoroutine(StunStatusCoroutine(stunDuration));
        }
        else
        {
            elapsedStunTime = 0f;
        }
    }

    public void ApplySlow(float slowAmount ,float slowDuration)
    {
        if (isSlowed == false)
        {
            StartCoroutine(SlowStatusCoroutine(slowAmount, slowDuration));
        }
        else
        {
            elapsedSlowTime = 0f;
        }
    }

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
                healthScript.TakeDamage(burnDamage, DamageSource.Neutral, 0);
                burnTickTimer = 0f;
            }

            elapsedBurnTime += Time.deltaTime;
            yield return null;
        }

        isOnFire = false;
    }

    IEnumerator StunStatusCoroutine(float stunDuration)
    {
        while (elapsedStunTime < stunDuration)
        {
            isStunned = true;
            elapsedStunTime += Time.deltaTime;
            yield return null;
        }

        isStunned = false;
    }

    IEnumerator SlowStatusCoroutine(float slowPercent, float slowDuration)
    {
        while (elapsedSlowTime < slowDuration)
        {
            slowAmount = slowPercent;
            elapsedSlowTime += Time.deltaTime;
            yield return null;
        }

        slowAmount = 1f;
    }
}
