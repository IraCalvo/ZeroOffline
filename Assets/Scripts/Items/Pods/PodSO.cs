using UnityEngine;

[CreateAssetMenu(menuName = "PodSO")]
public class PodSO : ScriptableObject
{
    public string podName;
    public float podBaseCD;
    public int podBaseDMG;
    public float podProjectileMS;
    public float podBaseCritChance;
}
