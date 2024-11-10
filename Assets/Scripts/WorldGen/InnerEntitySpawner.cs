using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerEntitySpawner : MonoBehaviour
{
    public void SpawnObject(Vector2Int spawnPos)
    {
        Vector3 spawnPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
        Instantiate(this.gameObject, spawnPosition, Quaternion.identity);    
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
