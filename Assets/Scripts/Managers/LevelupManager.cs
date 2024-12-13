using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class LevelupManager : MonoBehaviour
{
    public static LevelupManager instance;

    [SerializeField] GameObject pickMenu;

    public float commonRarityPercent;
    public float uncommonRarityPercent;
    public float rareRarityPercent;
    public float epicRarityPercent;
    public float legendaryRarityPercent;

    public List<LevelUpUpgradeBase> commonLevelup;
    public List<LevelUpUpgradeBase> uncommonLevelup;
    public List<LevelUpUpgradeBase> rareLevelup;
    public List<LevelUpUpgradeBase> epicLevelup;
    public List<LevelUpUpgradeBase> legendaryLevelup;

    public List<LevelupUpgradeButton> levelUpButtons;

    List<LevelUpUpgradeBase> duplicateLvlupChoice;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ShowLevelupOptions()
    {
        Time.timeScale = 0f;
        pickMenu.SetActive(true);
        ChooseUpgradeButtons();
    }

    void ChooseUpgradeButtons()
    { 
        for(int i = 0; i < levelUpButtons.Count; i++)
        {
            int randInt = FunctionUtils.RandomChance(0, 100);
            int randomUpgrade;
            LevelUpUpgradeBase levelupChosen = null;
            if (randInt < commonRarityPercent)
            {
                do
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, commonLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(commonLevelup[randomUpgrade]));

                levelupChosen = commonLevelup[randomUpgrade];
            }
            else if (randInt < commonRarityPercent + uncommonRarityPercent)
            {
                do 
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, uncommonLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(uncommonLevelup[randomUpgrade]));

                levelupChosen = uncommonLevelup[randomUpgrade];
            }
            else if (randInt < commonRarityPercent + uncommonRarityPercent + rareRarityPercent)
            {
                do
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, rareLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(rareLevelup[randomUpgrade]));

                levelupChosen = rareLevelup[randomUpgrade];
            }
            else if (randInt < commonRarityPercent + uncommonRarityPercent + rareRarityPercent + epicRarityPercent)
            {
                do
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, epicLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(epicLevelup[randomUpgrade]));

                levelupChosen = epicLevelup[randomUpgrade];
            }
            else
            {
                do
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, legendaryLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(legendaryLevelup[randomUpgrade]));

                levelupChosen = legendaryLevelup[randomUpgrade];
            }

            duplicateLvlupChoice.Add(levelupChosen);
            levelUpButtons[i].levelUpUpgrade.levelupUpgradeSO = levelupChosen.levelupUpgradeSO;
            levelUpButtons[i].levelupDescription.text = levelupChosen.levelupUpgradeSO.upgradeDescription;
            levelUpButtons[i].levelupName.text = levelupChosen.levelupUpgradeSO.upgradeName;
        }
    }

    public void UpgradeChosen()
    {
        pickMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RemoveUpgradeFromList(LevelUpUpgradeBase upgradeToRemove)
    {
        if (commonLevelup.Contains(upgradeToRemove))
        {
            commonLevelup.Remove(upgradeToRemove);
        }
        else if (uncommonLevelup.Contains(upgradeToRemove))
        {
            uncommonLevelup.Remove(upgradeToRemove);
        }
        else if (rareLevelup.Contains(upgradeToRemove))
        {
            rareLevelup.Remove(upgradeToRemove);
        }
        else if (epicLevelup.Contains(upgradeToRemove))
        {
            epicLevelup.Remove(upgradeToRemove);
        }
        else
        { 
            legendaryLevelup.Remove(upgradeToRemove);
        }

        //TODO: probably should add a list thatll re add them once the run is over so its not permanantely removed
    }

    public void ChangePercents()
    {

    }
}
