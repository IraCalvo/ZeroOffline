using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "OreSO")]
public class OreSO : ScriptableObject
{
    public ResourceType resourceType;
    public GameObject oreToDrop;
    [Range(0, 100)]
    public int minAmountToDrop;
    [Range(0, 100)]
    public int maxAmountToDrop;
    [Range(0, 100)]
    public int baseOreHealth;
}
