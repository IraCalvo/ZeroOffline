using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer, out HashSet<Vector2Int> walls)
    {
        // Generate one time
        var basicWallPos = FindWallsInDirections(floorPos, Direction2D.cardinalDirectionsList);
        var cornerWallPos = FindWallsInDirections(floorPos, Direction2D.diagonalDirectionsList);

        CreateBasicWalls(tilemapVisualizer, basicWallPos, floorPos);
        CreateCornerWalls(tilemapVisualizer, cornerWallPos, floorPos);

        var perimeterWalls = FindPerimeter(tilemapVisualizer, basicWallPos, cornerWallPos, floorPos);

        // Paint over again
        floorPos = FillFloorGaps(perimeterWalls);

        tilemapVisualizer.floorTilemap.ClearAllTiles();
        tilemapVisualizer.wallTilemap.ClearAllTiles();
        tilemapVisualizer.PaintFloorTiles(floorPos);

        basicWallPos = FindWallsInDirections(floorPos, Direction2D.cardinalDirectionsList);
        cornerWallPos = FindWallsInDirections(floorPos, Direction2D.diagonalDirectionsList);

        CreateBasicWalls(tilemapVisualizer, basicWallPos, floorPos);
        CreateCornerWalls(tilemapVisualizer, cornerWallPos, floorPos);

        walls = new HashSet<Vector2Int>();
        walls.UnionWith(basicWallPos);
        walls.UnionWith(cornerWallPos);
    }

    // Fill the floor through BFS
    private static HashSet<Vector2Int> FillFloorGaps(HashSet<Vector2Int> perimeterWalls)
    {
        HashSet<Vector2Int> floorPos = new HashSet<Vector2Int>();

        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        Queue<Vector2Int> queue = new Queue<Vector2Int>();
        Vector2Int startingPoint = new Vector2Int(0, 0);

        queue.Enqueue(startingPoint);

        while (queue.Count > 0)
        {
            Vector2Int currentPoint = queue.Dequeue();
            // If we already saw this point or we hit a wall then we stop searching
            if (visited.Contains(currentPoint) || perimeterWalls.Contains(currentPoint))
            {
                continue;
            }

            visited.Add(currentPoint);
            floorPos.Add(currentPoint);

            // Search all other directions
            queue.Enqueue(currentPoint + Vector2Int.up);
            queue.Enqueue(currentPoint + Vector2Int.down);
            queue.Enqueue(currentPoint + Vector2Int.left);
            queue.Enqueue(currentPoint + Vector2Int.right);
        }

        return floorPos;
    }

    private static HashSet<Vector2Int> FindPerimeter(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        Tilemap wallTileMap = tilemapVisualizer.wallTilemap;
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>(basicWallPos);
        wallPos.UnionWith(cornerWallPos);

        Vector2Int firstTile = new Vector2Int(0, 0);

        // Find starting point of outer wall
        BoundsInt boundsInt = wallTileMap.cellBounds;
        for (int y = boundsInt.yMax; y >= boundsInt.yMin; y--)
        {
            bool shouldBreak = false;
            for (int x = boundsInt.xMin; x <= boundsInt.xMax; x++)
            {
                Vector2Int position = new Vector2Int(x, y);
                if (wallPos.Contains(position))
                {
                    firstTile = new Vector2Int(x, y);
                    shouldBreak = true;
                    break;
                }
            }
            if (shouldBreak)
            {
                break;
            }
        }

        // Set the first currentTile
        Vector2Int currentTile = firstTile;
        if (wallPos.Contains(new Vector2Int(currentTile.x + 1, currentTile.y)))
        {
            currentTile = new Vector2Int(currentTile.x + 1, currentTile.y);
        }
        else if (wallPos.Contains(new Vector2Int(currentTile.x, currentTile.y - 1)))
        {
            currentTile = new Vector2Int(currentTile.x, currentTile.y - 1);
        }
        else if (wallPos.Contains(new Vector2Int(currentTile.x - 1, currentTile.y)))
        {
            currentTile = new Vector2Int(currentTile.x - 1, currentTile.y);
        }
        else if (wallPos.Contains(new Vector2Int(currentTile.x, currentTile.y + 1)))
        {
            currentTile = new Vector2Int(currentTile.x, currentTile.y + 1);
        }

        HashSet<Vector2Int> perimeterWall = FindPerimeterWall(wallPos, firstTile, currentTile);
        Debug.Log("FindPerimeter " + perimeterWall.Count);

        return perimeterWall;
    }

    private static HashSet<Vector2Int> FindPerimeterWall(HashSet<Vector2Int> wallPos, Vector2Int firstTile, Vector2Int currentTile)
    {
        HashSet<Vector2Int> perimeterTiles = new HashSet<Vector2Int>();
        HashSet<Vector2Int> seenTiles = new HashSet<Vector2Int>();
        perimeterTiles.Add(firstTile);
        perimeterTiles.Add(currentTile);
        seenTiles.Add(firstTile);
        seenTiles.Add(currentTile);

        // Starter code for the first tile
        // Look Right
        if (FindNextWallTile(wallPos, firstTile, new Vector2Int(currentTile.x + 1, currentTile.y), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(currentTile.x + 1, currentTile.y));
            return perimeterTiles;
        }
        // Look Down
        else if (FindNextWallTile(wallPos, firstTile, new Vector2Int(currentTile.x, currentTile.y - 1), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(currentTile.x, currentTile.y - 1));
            return perimeterTiles;
        }
        // Look Left
        else if (FindNextWallTile(wallPos, firstTile, new Vector2Int(currentTile.x - 1, currentTile.y), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(currentTile.x - 1, currentTile.y));
            return perimeterTiles;
        }
        // Look Up
        else if (FindNextWallTile(wallPos, firstTile, new Vector2Int(currentTile.x, currentTile.y + 1), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(currentTile.x, currentTile.y + 1));
            return perimeterTiles;
        }
        // Fail
        else
        {
            return new HashSet<Vector2Int>();
        }
    }

    private static bool FindNextWallTile(HashSet<Vector2Int> wallPos, Vector2Int firstTile, Vector2Int nextTile, HashSet<Vector2Int> seenTiles, HashSet<Vector2Int> perimeterTiles)
    {
        // If we arrive at the start then we can return everything is true
        if (nextTile == firstTile)
        {
            perimeterTiles.Add(nextTile);
            return true;
        }

        if (seenTiles.Contains(nextTile) ||
            !wallPos.Contains(nextTile))
        {
            seenTiles.Add(nextTile);
            return false;
        }
        seenTiles.Add(nextTile);

        // Recursive statement to look in each direction. When the nextTile == firstTile, this will recursively
        // call back with a bunch of trues and add to perimeterTiles
        if (FindNextWallTile(wallPos, firstTile, new Vector2Int(nextTile.x + 1, nextTile.y), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(nextTile.x + 1, nextTile.y));
            return true;
        }
        else if (FindNextWallTile(wallPos, firstTile, new Vector2Int(nextTile.x, nextTile.y - 1), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(nextTile.x, nextTile.y - 1));
            return true;
        }
        else if (FindNextWallTile(wallPos, firstTile, new Vector2Int(nextTile.x - 1, nextTile.y), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(nextTile.x - 1, nextTile.y));
            return true;
        }
        else if (FindNextWallTile(wallPos, firstTile, new Vector2Int(nextTile.x, nextTile.y + 1), seenTiles, perimeterTiles))
        {
            perimeterTiles.Add(new Vector2Int(nextTile.x, nextTile.y + 1));
            return true;
        }
        else
        {
            return false;
        }
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in cornerWallPos)
        {
            string neighborsBinaryValue = "";
            Debug.Log("WallGenerator, CreateCornerWalls: Evaluating Pos - " + pos);
            foreach (var dir in Direction2D.eightDirectionList)
            {
                var neighborPos = pos + dir;
                if (floorPos.Contains(neighborPos))
                {
                    neighborsBinaryValue += "1";
                    Debug.Log("WallGenerator, CreateCornerWalls: Evaluating Neighbor - " + neighborPos + " Binary Value: 1");

                }
                else
                {
                    neighborsBinaryValue += "0";
                    Debug.Log("WallGenerator, CreateCornerWalls: Evaluating Neighbor - " + neighborPos + " Binary Value: 0");

                }
            }
            tilemapVisualizer.PaintSingleCornerWall(pos, neighborsBinaryValue);
        }
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
        HashSet<Vector2Int> createdWalls = new HashSet<Vector2Int>();
        foreach (var pos in basicWallPos)
        {
            string neighborsBinaryValue = "";
            foreach (var dir in Direction2D.cardinalDirectionsList)
            {
                var neighborPos = pos + dir;
                if (floorPos.Contains(neighborPos))
                {
                    neighborsBinaryValue += "1";
                }
                else
                {
                    neighborsBinaryValue += "0";
                }
            }
            tilemapVisualizer.PaintSingleBasicWall(pos, neighborsBinaryValue);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPos, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPos = new HashSet<Vector2Int>();

        //only works for main cardinal directions not diagonals
        foreach (var pos in floorPos)
        {
            foreach (var direction in directionList)
            {
                var neighborPos = pos + direction;
                if (floorPos.Contains(neighborPos) == false)
                {
                    wallPos.Add(neighborPos);
                }
            }
        }

        return wallPos;
    }
}
