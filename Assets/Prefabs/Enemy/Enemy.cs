using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemySO enemySO;
    public float currentHP;

    private void OnEnable()
    {
        currentHP = enemySO.enemyBaseMaxHP;
    }

}
