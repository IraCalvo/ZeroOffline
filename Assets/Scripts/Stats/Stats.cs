using UnityEngine;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
    public Stat Health;
    public Stat Armor;
    public Stat MovementSpeed;
    public Stat ReloadTime;

    public List<Stat> statsList;

    private void Awake()
    {
        Health = new Stat("Health", StatType.Health, 10);
        Armor = new Stat("Armor", StatType.Armor, 0);
        MovementSpeed = new Stat("Movement Speed", StatType.MovementSpeed, 1);
        ReloadTime = new Stat("Reload Time", StatType.ReloadTime, 0);

        statsList = new List<Stat>() { Health, Armor, MovementSpeed, ReloadTime };

        foreach (Stat stat in statsList)
        {
            print(stat.GetStatInfo());
        }
    }
}
