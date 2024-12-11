using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "ClockModifier", menuName = "Scriptable Objects/ClockModifier")]

public class ClockModifier : ScriptableObject
{
    public string Name;
    public float Value;
    public StatType Type;
}
