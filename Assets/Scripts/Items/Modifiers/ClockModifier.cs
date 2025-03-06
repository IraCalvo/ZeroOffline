using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ClockModifier", menuName = "Scriptable Objects/ClockModifier")]

public class ClockModifier : ScriptableObject
{
    public string Name;
    public List<Stat> statIncrases;
    public List<Stat> statDecrases;
}
