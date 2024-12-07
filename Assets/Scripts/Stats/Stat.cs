using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private string statName;
    [SerializeField] private float statValue;

    public Stat(string statName, float statValue)
    {
        this.statName = statName;
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

    public void Add(float value)
    {
        statValue += value;
    }

    public void Subtract(float value)
    {
        statValue -= value;
    }
}
