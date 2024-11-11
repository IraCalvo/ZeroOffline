using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InnerDungeonGeneratorUtils : MonoBehaviour
{
    public static Vector2Int GetRandomSpawnPosition(HashSet<Vector2Int> floorPos)
    {
        Vector2Int[] floorArray = floorPos.ToArray();
        int randIndex = UnityEngine.Random.Range(0, floorArray.Length);
        return floorArray[randIndex];
    }

    public static bool CheckObjectCollision(Vector2Int spawnPos, InnerEntitySpawner obj, LayerMask layer)
    {
        InnerEntitySpawner innerEntityObj = obj.SpawnObject(spawnPos);
        Collider2D collider = innerEntityObj.GetComponent<Collider2D>();

        // Padding depends on the player size
        Vector2 padding = new Vector2(2, 2);

        Vector2 objSize = collider.bounds.size;
        Vector2 objCenter = collider.bounds.center;
        objSize += padding;
        innerEntityObj.DestroyObject();
        Debug.Log("CheckObjectCollision objName: " + obj.name + " spawnPos: " + spawnPos + " size: " + objSize + " center: " + objCenter);
        float rotation = 0;

        Collider2D hitColliders = Physics2D.OverlapBox(objCenter, objSize, rotation, layer);

        if (hitColliders == null)
        {
            return false;
        }
        else
        {
            Debug.Log("CheckObjectCollision hitCollider: ");
            return true;
        }
    }

}
