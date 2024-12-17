using UnityEngine;

[System.Serializable]
public class EnemyDrops
{
    [SerializeField] GameObject itemDrop;
    [SerializeField] float itemDropRate;
    [SerializeField] int minAmountToDrop;
    [SerializeField] int maxAmountToDrop;

    public EnemyDrops(GameObject itemDrop, float itemDropRate, int minAmountToDrop, int maxAmountToDrop)
    {
        this.itemDrop = itemDrop;
        this.itemDropRate = itemDropRate;
        this.minAmountToDrop = minAmountToDrop;
        this.maxAmountToDrop = maxAmountToDrop;
    }

    public GameObject GetItem()
    { 
        return itemDrop;
    }

    public float GetDropRate()
    { 
        return itemDropRate;
    }

    public int GetMinAmountToDrop() 
    { 
        return minAmountToDrop;
    }

    public int GetMaxAmountToDrop() 
    {
        return maxAmountToDrop;
    }
}
