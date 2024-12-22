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

public class OreNode : MonoBehaviour, IDamageable
{
    [SerializeField] OreSO oreSO;
    int currentOreHP;
    GameObject oreDrop;
    [SerializeField] float spawnRadius;
    [SerializeField] Material whiteFlashMaterial;

    void Awake()
    {
        currentOreHP = oreSO.baseOreHealth;
        oreDrop = oreSO.oreToDrop;
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
            StartCoroutine(WhiteFlashCoroutine());
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
            Vector2 spawnPos = FunctionUtils.GetRandomPositionInCircle(transform.position, spawnRadius);
            GameObject ore = ObjectPoolManager.Instance.GetPoolObject(oreDrop);
            ore.transform.position = transform.position;
            ore.GetComponent<ItemBounce>().StartBounce(spawnPos);
        }
        Destroy(gameObject);
    }

    IEnumerator WhiteFlashCoroutine()
    {
        Material startingMaterial = GetComponent<SpriteRenderer>().material;
        GetComponent<SpriteRenderer>().material = whiteFlashMaterial;
        yield return new WaitForSeconds(0.25f);
        GetComponent<SpriteRenderer>().material = startingMaterial;
    }
}
