using UnityEngine;
using TMPro;

public class LevelupUpgradeButton : MonoBehaviour
{
    public LevelUpUpgradeBase levelUpUpgrade;
    public TextMeshProUGUI levelupDescription;
    public TextMeshProUGUI levelupName;

    public void ButtonPressed()
    {
        levelUpUpgrade.ApplyUpgrade();
        LevelupManager.instance.UpgradeChosen();
    }
}
