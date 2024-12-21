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
    //the range in which they do their attack
    public float enemyAttackRange;
    //the range in which they aggro onto the player
    public float enemyAggroRange;
    public bool enemyNeedsConstantLOS;
    public float invulnTime;

    public List<EnemyDrops> itemDrops;
}
