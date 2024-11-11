using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerEntitySpawner : MonoBehaviour
{
    public InnerEntitySpawner SpawnObject(Vector2Int spawnPos)
    {
        Vector3 spawnPosition = new Vector3(spawnPos.x, spawnPos.y, 0);
        GameObject obj = Instantiate(this.gameObject, spawnPosition, Quaternion.identity);
        return obj.GetComponent<InnerEntitySpawner>();
    }

    public void DestroyObject()
    {
        DestroyImmediate(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Vector2 objSize = GetComponent<Collider2D>().bounds.size;
        objSize += new Vector2(2, 2);
        Vector2 objCenter = GetComponent<Collider2D>().bounds.center;

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(objCenter, objSize);
    }
}
