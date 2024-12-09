using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private string statName;
    [SerializeField] private float statValue;
    [SerializeField] private StatType statType;

    public Stat(string statName, StatType statType, float statValue)
    {
        this.statName = statName;
        this.statType = statType;
        this.statValue = statValue;
    }

    public string GetStatName()
    {
        return statName;
    }

    public float GetValue()
    {
        return statValue;
    }

    public string GetStatInfo()
    {
        return statName + ": " + statValue.ToString();
    }

    public StatType GetStatType()
    {
        return statType;
    }

    public void Add(float value)
    {
        statValue += value;
    }

    public void Subtract(float value)
    {
        statValue -= value;
    }
}
