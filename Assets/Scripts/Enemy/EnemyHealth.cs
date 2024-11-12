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
    SpriteRenderer sr;

    void OnEnable()
    { 
        enemy = GetComponent<Enemy>();
        currentEnemyHP = enemy.enemySO.enemyBaseMaxHP;
        sr = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damageInput, DamageSource dmgSource, float critChance)
    {
        if ((dmgSource == DamageSource.Player || dmgSource == DamageSource.Neutral) && canBeDamaged)
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
    }

    IEnumerator InvulnTimerCoroutine()
    {
        Material startMaterial = sr.material;
        sr.material = whiteDamageMaterial;
        yield return new WaitForSeconds(enemy.enemySO.invulnTime);
        sr.material = startMaterial;
        canBeDamaged = true;
    }
}
