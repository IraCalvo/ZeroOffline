using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class WallGenerator
{
    public static void CreateWalls(HashSet<Vector2Int> floorPos, TilemapVisualizer tilemapVisualizer)
    {
        var basicWallPos = FindWallsInDirections(floorPos, Direction2D.cardinalDirectionsList);
        var cornerWallPos = FindWallsInDirections(floorPos, Direction2D.diagonalDirectionsList);
        CreateBasicWalls(tilemapVisualizer, basicWallPos, floorPos);
        CreateCornerWalls(tilemapVisualizer, cornerWallPos, floorPos);
    }

    private static void CreateCornerWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        foreach (var pos in cornerWallPos) 
        {
            string neighborsBinaryValue = "";
            foreach (var dir in Direction2D.eightDirectionList)
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
            tilemapVisualizer.PaintSingleCornerWall(pos, neighborsBinaryValue);
        }
    }

    private static void CreateBasicWalls(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> floorPos)
    {
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
