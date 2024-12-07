using UnityEngine;
using System.Collections.Generic;

public class Stats : MonoBehaviour
{
    public Stat Health;
    public Stat Armor;
    public Stat MovementSpeed;
    public Stat ReloadTime;

    [SerializeField] private List<Stat> statsList;

    private void Awake()
    {
        Health = new Stat("Health", 10);
        Armor = new Stat("Armor", 0);
        MovementSpeed = new Stat("Movement Speed", 1);
        ReloadTime = new Stat("Reload Time", 0);

        statsList = new List<Stat>() { Health, Armor, MovementSpeed, ReloadTime };

        foreach (Stat stat in statsList)
        {
            print(stat.GetStatInfo());
        }
    }
}
