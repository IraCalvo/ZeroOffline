using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public static PlayerHealth instance;
    public int maxHealth;
    public int currentHealth;
    [SerializeField] public float invulnTimer;
    public bool playerCanBeHit;
    Material originalMaterial;
    [SerializeField] Material whiteFlashMaterial;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        originalMaterial = GetComponent<Material>();
    }

    public void TakeDamage(int dmgToTake, DamageSource dmgSource, float critChance)
    {
        if (playerCanBeHit)
        {
            if (currentHealth > 0)
            {
                if (dmgSource == DamageSource.Enemy || dmgSource == DamageSource.Neutral)
                {
                    currentHealth -= dmgToTake;
                    if (currentHealth < 0)
                    {
                        //TODO: Add game lose procedures in game manager
                        StartCoroutine(InvulnCountdown());
                    }
                }
            }
        }
    }

    IEnumerator InvulnCountdown()
    {
        playerCanBeHit = false;
        GetComponent<Renderer>().material = whiteFlashMaterial;
        yield return new WaitForSeconds(invulnTimer);
        GetComponent<Renderer>().material = originalMaterial;
        playerCanBeHit = true;
    }
}
