using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public int enemyBaseMaxHP;
    public float enemyBaseMS;
    public float enemyBaseDamage;
    public float enemyBaseRange;
    public bool enemyNeedsConstantLOS;
}
