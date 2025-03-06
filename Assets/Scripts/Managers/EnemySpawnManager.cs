using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{

    //list of potential spawns
    //spawn rates 
    public List<GameObject> enemiesToSpawn;
    public int minAmountOfEnemiesToSpawn;
    public int maxAmountOfEnemiesToSpawn;
    int spawnAmount;
    List<Vector2> floorPositions;
    

    public void AmountOfEnemieToSpawn()
    {
        spawnAmount = FunctionUtils.RandomChance(minAmountOfEnemiesToSpawn, maxAmountOfEnemiesToSpawn);
    }

    public void SpawnEnemies()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            
        }
    }
}
