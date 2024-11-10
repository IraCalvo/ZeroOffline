using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class InnerWallDungeonGenerator
{
    [SerializeField] static Tilemap floorTileMap;
    [SerializeField] static Tilemap wallTileMap;
    [SerializeField] static List<InnerEntitySpawner> wallPrefabList;
    [Range(0, 100)]
    [SerializeField] static int amountOfWallsToGenerate;
    [SerializeField] static LayerMask layersToNotSpawnOn;
    [SerializeField] static int spawnRetries;


    public static void InnerWallGenerator(HashSet<Vector2Int> floorPos)
    {
        InnerEntitySpawner wallPrefab = null;
        for (int i = 0; i < amountOfWallsToGenerate; i++)
        {
            int wall = Random.Range(0, wallPrefabList.Count);
            wallPrefab = wallPrefabList[wall];


            CheckIfValidWallSpawn(wallPrefab, floorPos);
        }
    }

    static void CheckIfValidWallSpawn(InnerEntitySpawner wall, HashSet<Vector2Int> floorPos)
    {
        int retries = 0;

        //Get random spawn pos, spawn it on said spawnPos, then check if it collides, if not retry, until give up
        while (retries < spawnRetries)
        {
            Vector2Int spawnPos = InnerDungeonGeneratorUtils.GetRandomSpawnPosition(floorPos);
            wall.SpawnObject(spawnPos);

            if (InnerDungeonGeneratorUtils.CheckObjectCollision(wall.gameObject))
            {
                wall.DestroyObject();
                retries++;
            }
            else
            {
                break;
            }
        }
    }

    //Vector3 GetRandomSpawnPosition(GameObject wallPrefab)
    //{
    //    int attempts = 0;
    //    Vector3 spawnPos = Vector3.zero;
    //    Collider2D wallCollider = wallPrefab.GetComponent<Collider2D>();

    //    while (attempts < spawnRetries)
    //    { 
    //        List<Vector3> validTilePos = new List<Vector3>();
    //        BoundsInt bounds = floorTileMap.cellBounds;

    //        int randomX = Random.Range(bounds.xMin, bounds.xMax);
    //        int randomY = Random.Range(bounds.yMin, bounds.yMax);

    //        Vector3 worldPos = floorTileMap.CellToWorld(new Vector3Int(randomX, randomY, 0));


    //        if (ValidInnerWallPosition(worldPos, wallCollider))
    //        {
    //            spawnPos = worldPos;
    //            break;
    //        }
    //        attempts++;
    //    }
    //}

    //bool ValidInnerWallPosition(Vector3 posToCheck, Collider2D wallCollider)
    //{
    //    ContactFilter2D filter = new ContactFilter2D();
    //    filter.SetLayerMask(layersToNotSpawnOn);
    //    filter.useTriggers = false;

    //    List<Collider2D> overlaps = new List<Collider2D>();

    //    int overlapCount = wallCollider.OverlapCollider(filter, overlaps);
    //    if (overlapCount > 0)
    //    {
    //        return false;
    //    }

    //    return true;
    //}
}
