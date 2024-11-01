using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
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

        FindPerimeter(tilemapVisualizer, basicWallPos, cornerWallPos, floorPos);


        var walkedFloorPos = FillFloorGaps(floorPos, basicWallPos, cornerWallPos, tilemapVisualizer);

        floorPos.UnionWith(walkedFloorPos);
        basicWallPos = FindWallsInDirections(floorPos, Direction2D.cardinalDirectionsList);
        cornerWallPos = FindWallsInDirections(floorPos, Direction2D.diagonalDirectionsList);

        // Paint over again
        tilemapVisualizer.PaintFloorTiles(floorPos);
        CreateBasicWalls(tilemapVisualizer, basicWallPos, floorPos);
        CreateCornerWalls(tilemapVisualizer, cornerWallPos, floorPos);
        RemoveFullWalls(tilemapVisualizer);

    }

    private static HashSet<Vector2Int> FillFloorGaps(HashSet<Vector2Int> floorPos, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> cornerWallPos, TilemapVisualizer tilemapVisualizer) {
        // Get the highest and lowest rows
        // From high to low, iterate through each row
        // Find the min and max x of this row
        // Start Walker: Can't spawn it on the walls or on empty spaces
        // Walk right placing tiles
        // Check if there is a wall, if there is die
        //
        HashSet<Vector2Int> walkedFloorPositions = new HashSet<Vector2Int>();
        int highestRowY = tilemapVisualizer.wallTilemap.cellBounds.yMax;
        int lowestRowY = tilemapVisualizer.wallTilemap.cellBounds.yMin;
        int highestColumnX = tilemapVisualizer.wallTilemap.cellBounds.xMax;
        int lowestColumnX = tilemapVisualizer.wallTilemap.cellBounds.xMin;

        for (int y = highestRowY; y >= lowestRowY; y--)
        {
            for (int x = lowestColumnX; x <= highestColumnX; x++)
            {
                Vector2Int currentPosition = new Vector2Int(x, y);
                if (basicWallPos.Contains(currentPosition) || cornerWallPos.Contains(currentPosition))
                {
                    if (tilemapVisualizer.wallTilemap.GetSprite((Vector3Int)currentPosition) == null)
                    {
                        walkedFloorPositions.Add(currentPosition);
                    }
                }
            }

        }

        return walkedFloorPositions;
    }

    private static void RemoveFullWalls(TilemapVisualizer tilemapVisualizer)
    {
        Tilemap floorTileMap = tilemapVisualizer.floorTilemap;
        Tilemap wallTileMap = tilemapVisualizer.wallTilemap;

        BoundsInt bounds = wallTileMap.cellBounds;
        for (int y = bounds.yMax; y >= bounds.yMin; y--)
        {
            for (int x = bounds.xMin; x <= bounds.xMax; x++)
            {
                Vector3Int position = new Vector3Int(x, y);
                TileBase tile = wallTileMap.GetTile(position);

                if (tile != null && tile.name == tilemapVisualizer.wallFull.name)
                {
                    Debug.Log("RemoveFullWalls: " + tile.name);
                    TileData tileData = new TileData();
                    tile.GetTileData(position, wallTileMap, ref tileData);

                    TileData floorTileData = new TileData();
                    

                    //floorTileMap.SetTile(position, tileData);
                }
            }
        }
    }

    private static void FindPerimeter(TilemapVisualizer tilemapVisualizer, HashSet<Vector2Int> basicWallPos, HashSet<Vector2Int> cornerWallPos, HashSet<Vector2Int> floorPos)
    {
        Tilemap wallTileMap = tilemapVisualizer.wallTilemap;

        Vector2Int firstTile = new Vector2Int(0, 0);

        // Find starting point of outer wall
        BoundsInt boundsInt = wallTileMap.cellBounds;
        for (int y = boundsInt.yMax; y >= boundsInt.yMin; y--)
        {
            bool shouldBreak = false;
            for (int x = boundsInt.xMin; x <= boundsInt.xMax; x++)
            {
                Vector3Int position = new Vector3Int(x, y);
                TileBase tile = wallTileMap.GetTile(position);
                if (tile != null && tile.name == tilemapVisualizer.wallFull.name)
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

        Vector2Int currentTile = firstTile;

        HashSet<Vector2Int> perimeterWall = FindPerimeterWall(wallTileMap, firstTile, currentTile);
        // Find the first next tile to kick off the loop.
        //currentTile = FindNextWallTile(wallTileMap, currentTile, seenTiles, perimeterTiles);
        //perimeterTiles.Add(currentTile);
        //while (currentTile != firstTile)
        //{
        //    currentTile = FindNextWallTile(wallTileMap, currentTile, seenTiles, perimeterTiles);
        //    if (currentTile == new Vector2Int(0, 0))
        //    {
        //        // Stop finding the perimeter, should never happen. Crash the game here
        //        Debug.Log("FindPerimeter Broke");
        //        Debug.Log("FindPerimeter seenTiles: " + seenTiles);
        //        Debug.Log("FindPerimeter perimeterTiles: " + perimeterTiles);
        //        return;
        //    }
        //    perimeterTiles.Add(currentTile);
        //}

        // Find the tiles that are not contained in perimeter tiles and remove them
        //Debug.Log("FindPerimeter" + perimeterTiles);
    }

    private static HashSet<Vector2Int> FindPerimeterWall(Tilemap wallTileMap, Vector2Int firstTile, Vector2Int currentTile)
    {
        HashSet<Vector2Int> perimeterTiles = new HashSet<Vector2Int>();
        HashSet<Vector2Int> seenTiles = new HashSet<Vector2Int>();
        if (FindNextWallTile(wallTileMap, firstTile, currentTile, new Vector2Int(currentTile.x + 1, currentTile.y), seenTiles, perimeterTiles)
            || FindNextWallTile(wallTileMap, firstTile, currentTile, new Vector2Int(currentTile.x, currentTile.y - 1), seenTiles, perimeterTiles)
            || FindNextWallTile(wallTileMap, firstTile, currentTile, new Vector2Int(currentTile.x - 1, currentTile.y), seenTiles, perimeterTiles)
            || FindNextWallTile(wallTileMap, firstTile, currentTile, new Vector2Int(currentTile.x, currentTile.y + 1), seenTiles, perimeterTiles))
        {
            return perimeterTiles;
        } else
        {
            return new HashSet<Vector2Int>();
        }
    }

    private static bool FindNextWallTile(Tilemap wallTileMap, Vector2Int firstTile, Vector2Int currentTile, Vector2Int nextTile, HashSet<Vector2Int> seenTiles, HashSet<Vector2Int> perimeterTiles)
    {
        // Find the next tile

        if (currentTile == firstTile)
        {
            return true;
        }

        if (!seenTiles.Contains(nextTile) &&
            !perimeterTiles.Contains(nextTile) &&
            wallTileMap.GetTile(new Vector3Int(nextTile.x, nextTile.y, 0)) == null)
        {
            seenTiles.Add(nextTile);
            return false;
        }
        seenTiles.Add(nextTile);
        perimeterTiles.Add(nextTile);


        return FindNextWallTile(wallTileMap, firstTile, nextTile, new Vector2Int(nextTile.x + 1, nextTile.y), seenTiles, perimeterTiles) 
            || FindNextWallTile(wallTileMap, firstTile, nextTile, new Vector2Int(nextTile.x, nextTile.y - 1), seenTiles, perimeterTiles) 
            || FindNextWallTile(wallTileMap, firstTile, nextTile, new Vector2Int(nextTile.x - 1, nextTile.y), seenTiles, perimeterTiles) 
            || FindNextWallTile(wallTileMap, firstTile, nextTile, new Vector2Int(nextTile.x, nextTile.y + 1), seenTiles, perimeterTiles);


        // We will look starting right and then clockwise
        // Look Right
        //Vector2Int nextTile = new Vector2Int(currentTile.x + 1, currentTile.y);
        //if (!seenTiles.Contains(nextTile) &&
        //    !perimeterTiles.Contains(nextTile) &&
        //    wallTileMap.GetTile(new Vector3Int(nextTile.x, nextTile.y, 0)) != null)
        //{
        //    return nextTile;
        //}
        //else
        //{
        //    seenTiles.Add(nextTile);
        //}


        //// Look Down
        //nextTile = new Vector2Int(currentTile.x, currentTile.y - 1);
        //if (!seenTiles.Contains(nextTile) &&
        //    !perimeterTiles.Contains(nextTile) && 
        //    wallTileMap.GetTile(new Vector3Int(nextTile.x, nextTile.y, 0)) != null)
        //{
        //    return nextTile;
        //}
        //else
        //{
        //    seenTiles.Add(nextTile);
        //}

        //// Look Left
        //nextTile = new Vector2Int(currentTile.x - 1, currentTile.y);
        //if (!seenTiles.Contains(nextTile) &&
        //    !perimeterTiles.Contains(nextTile) && 
        //    wallTileMap.GetTile(new Vector3Int(nextTile.x, nextTile.y, 0)) != null)
        //{
        //    return nextTile;
        //}
        //else
        //{
        //    seenTiles.Add(nextTile);
        //}

        //// Look Up
        //nextTile = new Vector2Int(currentTile.x, currentTile.y + 1);
        //if (!seenTiles.Contains(nextTile) &&
        //    !perimeterTiles.Contains(nextTile) && 
        //    wallTileMap.GetTile(new Vector3Int(nextTile.x, nextTile.y, 0)) != null)
        //{
        //    return nextTile;
        //}
        //else
        //{
        //    seenTiles.Add(nextTile);
        //}

        // Failed to find perimeter, should never happen
        // Crash the game
        return new Vector2Int(0, 0);
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
