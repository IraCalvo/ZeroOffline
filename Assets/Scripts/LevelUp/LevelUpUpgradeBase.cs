using UnityEngine;

public class LevelUpUpgradeBase : MonoBehaviour
{
    public LevelUpUpgradeSO levelupUpgradeSO;

    public void ApplyUpgrade()
    {
        for (int i = 0; i < levelupUpgradeSO.statsToIncrease.Count; i++)
        {
            for (int j = 0; j < PlayerStats.instance.statsList.Count; j++)
            {
                if (PlayerStats.instance.statsList[j].GetStatType() ==
                    levelupUpgradeSO.statsToIncrease[i].GetStatType())
                {
                    PlayerStats.instance.statsList[j].Add(levelupUpgradeSO.statsToIncrease[i].GetValue());
                }
            }
        }

        for (int i = 0; i < levelupUpgradeSO.statsToDecrease.Count; i++)
        {
            for (int j = 0; j < PlayerStats.instance.statsList.Count; j++)
            {
                if (PlayerStats.instance.statsList[j].GetStatType() ==
                    levelupUpgradeSO.statsToDecrease[i].GetStatType())
                {
                    PlayerStats.instance.statsList[j].Subtract(levelupUpgradeSO.statsToDecrease[i].GetValue());
                }
            }
        }
    }
}
