using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InnerWallDungeonGenerator : MonoBehaviour
{
    HashSet<Vector2Int> floorPos;
    public InnerDungeonWallSO innerWallGenSO;

    public InnerWallDungeonGenerator(HashSet<Vector2Int> floorPos, InnerDungeonWallSO innerWallGenSO)
    {
        this.innerWallGenSO = innerWallGenSO;
        this.floorPos = floorPos;
    }

    public static void InnerWallGenerator(HashSet<Vector2Int> floorPos, InnerDungeonWallSO innerWallGenSO)
    {
        InnerEntitySpawner wallPrefab = null;
        for (int i = 0; i < innerWallGenSO.wallGenerationAmount; i++)
        {
            int wall = Random.Range(0, innerWallGenSO.wallPrefabList.Count);
            wallPrefab = innerWallGenSO.wallPrefabList[wall];


            CheckIfValidWallSpawn(wallPrefab, floorPos, innerWallGenSO);
        }
    }

    static void CheckIfValidWallSpawn(InnerEntitySpawner wall, HashSet<Vector2Int> floorPos, InnerDungeonWallSO innerWallGenSO)
    {
        int retries = 0;

        //Get random spawn pos, spawn it on said spawnPos, then check if it collides, if not retry, until give up
        while (retries < innerWallGenSO.spawnRetries)
        {
            Vector2Int spawnPos = InnerDungeonGeneratorUtils.GetRandomSpawnPosition(floorPos);

            if (InnerDungeonGeneratorUtils.CheckObjectCollision(spawnPos, wall, innerWallGenSO.layersToNotSpawnOn))
            {
                Debug.Log("Destroy wall");
                retries++;
            }
            else
            {
                wall.SpawnObject(spawnPos);
                retries = 0;
                break;
            }
        }
    }
}
