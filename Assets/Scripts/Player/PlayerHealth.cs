using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public static PlayerHealth instance;
    public int maxHealth;
    public int currentHealth;
    [SerializeField] public float invulnTimer;
    public bool playerCanBeHit = true;
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
        originalMaterial = GetComponent<SpriteRenderer>().material;
        currentHealth = maxHealth;
    }

    void Start()
    {
        PlayerBattleUIManager.instance.UpdateHPBar(currentHealth, maxHealth);
    }

    public void TakeDamage(int dmgToTake, DamageOwner dmgSource, float critChance)
    {
        if (playerCanBeHit)
        {
            if (currentHealth > 0)
            {
                if (dmgSource == DamageOwner.Enemy || dmgSource == DamageOwner.Neutral)
                {
                    currentHealth -= dmgToTake;
                    StartCoroutine(InvulnCountdown());
                    PlayerBattleUIManager.instance.UpdateHPBar(currentHealth, maxHealth);
                    if (currentHealth < 0)
                    {
                        //TODO: Add game lose procedures in game manager
                    }
                }
            }
        }
    }

    IEnumerator InvulnCountdown()
    {
        playerCanBeHit = false;
        GetComponent<SpriteRenderer>().material = whiteFlashMaterial;
        yield return new WaitForSeconds(invulnTimer);
        GetComponent<SpriteRenderer>().material = originalMaterial;
        playerCanBeHit = true;
    }
}
