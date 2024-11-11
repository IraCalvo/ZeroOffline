using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleRandomWalkDungeonGenerator : AbstractDungeonGenerator
{
    [SerializeField] protected SimpleRandomWalkSO randomWalkParameters;
    [SerializeField] protected InnerDungeonWallSO innerWallGenerationSO;

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPos = RunRandomWalk(randomWalkParameters, startPos);
        tilemapVisualizer.ClearTilemap();
        tilemapVisualizer.PaintFloorTiles(floorPos);


        HashSet<Vector2Int> walls = new HashSet<Vector2Int>();
        

        WallGenerator.CreateWalls(floorPos, tilemapVisualizer, out walls);

        // HACKY SOLUTION LMAO:
        // Create walls because we can't figure out why the inner walls can't see the outer
        // wall colliders
        List<GameObject> createdWallGameObjects = new List<GameObject>();
        // Don't spawn at 0,0 lmao
        walls.Add(new Vector2Int(0, 0));
        foreach (Vector2Int wall in walls)
        {
            createdWallGameObjects.Add(CreateEmptyObject(wall));
        }

        InnerWallDungeonGenerator.InnerWallGenerator(floorPos, innerWallGenerationSO);

        // DELETE THE CREATED WALL HACK LMAO
        foreach (GameObject wall in createdWallGameObjects)
        {
            DestroyImmediate(wall);
        }
    }

    public GameObject CreateEmptyObject(Vector2Int position)
    {
        Vector3 spawnPosition = new Vector3(position.x, position.y, 0f);

        GameObject newObject = new GameObject("EmptyObjectWithCollider");
        newObject.transform.position = spawnPosition;
        newObject.layer = LayerMask.NameToLayer("Walls");

        BoxCollider2D collider = newObject.AddComponent<BoxCollider2D>();

        collider.size = new Vector2(1f, 1f);

        return newObject;
    }


    protected HashSet<Vector2Int> RunRandomWalk(SimpleRandomWalkSO parameters, Vector2Int position)
    {
        var currentPos = position;
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();
        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = ProceduralGeneration.SimpleRandomWalk(currentPos, parameters.walkLength);

            //add paths and makes sures its not a duplicate
            floorPos.UnionWith(path);

            if (parameters.startRandomlyEachIteration)
            {
                currentPos = floorPos.ElementAt(Random.Range(0, floorPos.Count));
            }
        }

        return floorPos;
    }
}
