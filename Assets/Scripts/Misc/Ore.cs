using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{ 
    Copper,
    Silver,
    Gold,
    Topaz,
    Ruby,
    Emerald,
    Ammethyst,
    Diamond
}

public class Ore : MonoBehaviour, IDamageable
{
    OreSO oreSO;
    int currentOreHP;
    GameObject oreDrop;
    [SerializeField] float spawnRadius;

    void Awake()
    {
        currentOreHP = oreSO.baseOreHealth;
    }

    public void TakeDamage(int damageToTake, DamageSource dmgSource, float critChance)
    {
        if (dmgSource == DamageSource.Player)
        {
            bool isCrit = false;
            float roll = Random.Range(0f, 100f);

            if (roll <= critChance)
            {
                isCrit = true;
            }

            if(isCrit)
            {
                damageToTake *= 2;
            }

            currentOreHP -= damageToTake;
            if (currentOreHP <= 0)
            {
                DestroyOre();
            }
        }
    }

    void DestroyOre()
    {
        int dropAmount = Random.Range(oreSO.minAmountToDrop, oreSO.maxAmountToDrop);

        for (int i = 0; i < dropAmount; i++)
        {
            Vector2 spawnPos = GetRandomPositionInCircle(transform.position, spawnRadius);
            GameObject ore = ObjectPoolManager.Instance.GetPoolObject(oreDrop);
            ore.transform.position = spawnPos;
        }
    }

    private Vector2 GetRandomPositionInCircle(Vector2 center, float radius)
    {
        float angle = Random.Range(0f, 2f * Mathf.PI);
        float distance = Random.Range(0f, radius);

        float x = center.x + Mathf.Cos(angle) * distance;
        float y = center.y + Mathf.Sin(angle) * distance;

        return new Vector2(x, y);
    }
}
