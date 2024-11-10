using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OreSO : ScriptableObject
{
    public ResourceType resourceType;
    [Range(0, 100)]
    public int minAmountToDrop;
    [Range(0, 100)]
    public int maxAmountToDrop;
    [Range(0, 100)]
    public int baseOreHealth;
}
