using System.Collections.Generic;
using UnityEngine;

public class ClockModifierManager : MonoBehaviour
{
    public static ClockModifierManager Instance { get; private set; }

    public List<ClockModifier> List;
    private void Awake()
    {
        Instance = this;
    }

    public ClockModifier GetRandomClockModifier()
    {
        return List[Random.Range(0,List.Count)];
    }
}
