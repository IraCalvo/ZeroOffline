using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Inner Dungeon Generator")]
public class InnerDungeonWallSO : ScriptableObject
{
    public List<InnerEntitySpawner> wallPrefabList;
    [Range(0, 100)]
    public int wallGenerationAmount;
    public LayerMask layersToNotSpawnOn;
    public int spawnRetries;
}
