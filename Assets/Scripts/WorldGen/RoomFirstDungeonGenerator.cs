using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField] private int minRoomWidth;
    [SerializeField] private int minRoomHeight;

    [SerializeField] private int dungeonWidth;
    [SerializeField] private int dungeonHeight;

    [Range(0 , 10)]
    [SerializeField] private int offset;
    [SerializeField] private bool randomWalkRooms;

    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGeneration.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPos, 
            new Vector3Int(dungeonWidth, dungeonHeight, 0)), 
            minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else 
        {
            floor = CreateSimpleRoom(roomsList);
        }

        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for(int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            foreach (var pos in roomFloor)
            {
                if (pos.x >= (roomBounds.xMin + offset) && pos.x <= (roomBounds.xMax - offset)
                    && pos.y >= (roomBounds.yMin - offset) && pos.y <= (roomBounds.yMax - offset))
                { 
                    floor.Add(pos);
                }
            }
        }

        return floor;
    }

    //if want to dectorate procedurally, save room after its been created in a nother List/Hashset to come back to later then generate decor
    private HashSet<Vector2Int> CreateSimpleRoom(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int pos = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(pos);
                }
            }
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var pos = currentRoomCenter;
        corridor.Add(pos);
        while (pos.y != destination.y)
        {
            if (destination.y > pos.y)
            {
                pos += Vector2Int.up;
            }
            else if (destination.y < pos.y)
            {
                pos += Vector2Int.down;
            }
            corridor.Add(pos);
        }
        while (pos.x != destination.x)
        {
            if (destination.x > pos.x)
            {
                pos += Vector2Int.right;
            }
            else if (destination.x < pos.x)
            {
                pos += Vector2Int.left;
            }
            corridor.Add(pos);
        }

        return corridor;
    }
}
