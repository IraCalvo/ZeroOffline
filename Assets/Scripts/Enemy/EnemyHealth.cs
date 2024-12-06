using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    Enemy enemy;
    int currentEnemyHP;
    bool canBeDamaged = true;
    [SerializeField] GameObject dmgTakenText;
    [SerializeField] Transform positionToDisplayDamageText;
    [SerializeField] Material whiteDamageMaterial;
    [SerializeField] int xpToDrop;
    SpriteRenderer sr;

    void OnEnable()
    { 
        enemy = GetComponent<Enemy>();
        currentEnemyHP = enemy.enemySO.enemyBaseMaxHP;
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damageInput, DamageSource dmgSource, float critChance)
    {
        if (dmgSource == DamageSource.Neutral)
        {
            CalculateDamage(damageInput, critChance);
        }
        else if (dmgSource == DamageSource.Player && canBeDamaged)
        {
            canBeDamaged = false;
            StartCoroutine(InvulnTimerCoroutine());
            CalculateDamage(damageInput, critChance);
        }
    }

    public void CalculateDamage(int dmgInput, float critChance)
    {
        bool isCrit = false;
        float roll = Random.Range(0f, 100f);
        if (roll <= critChance)
        {
            isCrit = true;
        }

        if (isCrit)
        {
            dmgInput *= 2;
        }

        currentEnemyHP -= dmgInput;
        DisplayDamage(dmgInput, isCrit);
        if (currentEnemyHP <= 0)
        {
            DeathProcedures();
        }
    }

    void DisplayDamage(int damageToDisplay, bool crit)
    {
        GameObject damageTxt = ObjectPoolManager.Instance.GetPoolObject(PoolObjectType.DamagePopup);
        damageTxt.transform.position = positionToDisplayDamageText.transform.position;
        damageTxt.GetComponent<DamagePopup>().ShowPopup(damageToDisplay, crit);
    }

    void DeathProcedures()
    {
        ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
        for (int i = 0; i < xpToDrop; i++)
        {
            Vector2 spawnPos = GetRandomPosInCircle(transform.position, 1.5f);
            GameObject xp = ObjectPoolManager.Instance.GetPoolObject(PoolObjectType.XP);
            xp.transform.position = spawnPos;
            xp.GetComponent<ItemBounce>().StartBounce(spawnPos);
        }
    }

    IEnumerator InvulnTimerCoroutine()
    {
        Material startMaterial = sr.material;
        sr.material = whiteDamageMaterial;
        yield return new WaitForSeconds(enemy.enemySO.invulnTime);
        sr.material = startMaterial;
        canBeDamaged = true;
    }

    private Vector2 GetRandomPosInCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float distance = Random.Range(0f, radius);

        float x = center.x + Mathf.Cos(angle) * distance;
        float y = center.y + Mathf.Sin(angle) * distance;

        return  new Vector2(x, y);
    }
}
