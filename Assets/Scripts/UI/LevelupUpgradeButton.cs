using UnityEngine;
using TMPro;

public class LevelupUpgradeButton : MonoBehaviour
{
    public LevelUpUpgradeSO levelUpUpgradeSO;
    public TextMeshProUGUI levelupDescription;
    public TextMeshProUGUI levelupName;

    public void ButtonPressed()
    {
        LevelupManager.instance.ApplyUpgrade(levelUpUpgradeSO);
        LevelupManager.instance.UpgradeChosen();
    }
}
