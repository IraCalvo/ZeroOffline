using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public float enemyBaseMaxHP;
    public float enemyBaseMS;
    public float enemyBaseDamage;
    public List<GameObject> enemyDrops;
}
