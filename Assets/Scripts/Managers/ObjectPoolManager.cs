using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolObjectType
{ 
    Enemy,
    Projectile,
    Resource,
    DamagePopup
}

[Serializable]
public class PoolInfo
{
    public string name;
    public PoolObjectType type;
    public int amountToPool;
    public GameObject prefabToPool;
    public GameObject objectContainer;
    public bool poolCanGrow;

    [HideInInspector]
    public List<GameObject> pool = new List<GameObject>();
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    [SerializeField]
    public List<PoolInfo> listOfPools;
    private Vector3 defaultPos = new Vector3(0,0,0);


    private void Start()
    {
        for (int i = 0; i < listOfPools.Count; i++)
        {
            FillPool(listOfPools[i]);
        }
    }

    void FillPool(PoolInfo info)
    {
        for (int i = 0; i < info.amountToPool; i++)
        {
            GameObject objInstance;
            objInstance = Instantiate(info.prefabToPool, info.objectContainer.transform);
            objInstance.SetActive(false);
            objInstance.transform.position = defaultPos;
            info.pool.Add(objInstance);
        }
    }

    public GameObject GetPoolObject(GameObject obj)
    {
        PoolInfo selected = null;
        GameObject objInstance = null;

        if (obj.TryGetComponent<Projectile>(out Projectile proj))
        {
            selected = GetPoolByProjectileSprite(proj.GetComponent<SpriteRenderer>().sprite);
        }
        else if(obj.TryGetComponent<Ore>(out Ore ore))
        {
            selected = GetPoolByOreSprite(ore.GetComponent<SpriteRenderer>().sprite);
        }

        List<GameObject> pool = selected.pool;
        List<GameObject> nonActiveObjectsInPool = new();

        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                nonActiveObjectsInPool.Add(pool[i]);
            }
        }

        if (nonActiveObjectsInPool.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, nonActiveObjectsInPool.Count);
            objInstance = nonActiveObjectsInPool[randomIndex];
            objInstance.SetActive(true);
        }
        else if (nonActiveObjectsInPool.Count == 0 && selected.poolCanGrow)
        {
            objInstance = Instantiate(selected.prefabToPool, selected.objectContainer.transform);
            objInstance.SetActive(true);
        }

        return objInstance;
    }

    public GameObject GetPoolObject(PoolObjectType type)
    {
        PoolInfo selected = GetPoolByType(type);
        List<GameObject> pool = selected.pool;
        List<GameObject> nonActiveObjectsInPool = new();
        GameObject objInstance = null;

        for (int i = 0; i < pool.Count; i++)
        {
            if (pool[i].activeInHierarchy == false)
            {
                nonActiveObjectsInPool.Add(pool[i]);
            }
        }

        if (nonActiveObjectsInPool.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, nonActiveObjectsInPool.Count);
            objInstance = nonActiveObjectsInPool[randomIndex];
            objInstance.SetActive(true);
        }
        else if (nonActiveObjectsInPool.Count == 0 && selected.poolCanGrow)
        {
            objInstance = Instantiate(selected.prefabToPool, selected.objectContainer.transform);
            objInstance.SetActive(true);
        }

        return objInstance;
    }

    public void DeactivateObjectInPool(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = defaultPos;
    }

    public PoolInfo GetPoolByType(PoolObjectType type)
    {
        for (int i = 0; i < listOfPools.Count; i++)
        {
            if (type == listOfPools[i].type)
            {
                return listOfPools[i];
            }
        }

        return null;
    }

    public PoolInfo GetPoolByProjectileSprite(Sprite projSprite)
    {
        foreach (PoolInfo poolInfo in listOfPools)
        {
            if (poolInfo.type == PoolObjectType.Projectile)
            {
                Sprite tempSprite = poolInfo.prefabToPool.GetComponent<SpriteRenderer>().sprite;
                if (projSprite == tempSprite)
                {
                    return poolInfo;
                }
            }
        }
        return null;
    }

    private PoolInfo GetPoolByOreSprite(Sprite oreSprite)
    {
        foreach (PoolInfo poolInfo in listOfPools)
        {
            if (poolInfo.type == PoolObjectType.Resource)
            {
                Sprite tempSprite = poolInfo.prefabToPool.GetComponent<SpriteRenderer>().sprite;
                if (oreSprite == tempSprite)
                {
                    return poolInfo;
                }
            }
        }
        return null;
    }
}
