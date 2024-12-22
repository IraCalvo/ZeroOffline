using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelupManager : MonoBehaviour
{
    public static LevelupManager instance;

    [SerializeField] GameObject pickMenu;
    public int rerollAmount;
    public TextMeshProUGUI rerollsLeftText;

    public float commonRarityPercent;
    public float uncommonRarityPercent;
    public float rareRarityPercent;
    public float epicRarityPercent;
    public float legendaryRarityPercent;

    public List<LevelUpUpgradeSO> commonLevelup;
    public List<LevelUpUpgradeSO> uncommonLevelup;
    public List<LevelUpUpgradeSO> rareLevelup;
    public List<LevelUpUpgradeSO> epicLevelup;
    public List<LevelUpUpgradeSO> legendaryLevelup;

    public List<LevelupUpgradeButton> levelUpButtons;

    List<LevelUpUpgradeSO> duplicateLvlupChoice;

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
        rerollsLeftText.text = rerollAmount.ToString();
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
            LevelUpUpgradeSO levelupChosen = null;
            if (randInt > commonRarityPercent)
            {
                do
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, commonLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(commonLevelup[randomUpgrade]));

                levelupChosen = commonLevelup[randomUpgrade];
            }
            else if (randInt > commonRarityPercent + uncommonRarityPercent)
            {
                do 
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, uncommonLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(uncommonLevelup[randomUpgrade]));

                levelupChosen = uncommonLevelup[randomUpgrade];
            }
            else if (randInt > commonRarityPercent + uncommonRarityPercent + rareRarityPercent)
            {
                do
                {
                    randomUpgrade = FunctionUtils.RandomChance(0, rareLevelup.Count);
                }
                while (duplicateLvlupChoice.Contains(rareLevelup[randomUpgrade]));

                levelupChosen = rareLevelup[randomUpgrade];
            }
            else if (randInt > commonRarityPercent + uncommonRarityPercent + rareRarityPercent + epicRarityPercent)
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
            levelUpButtons[i].levelUpUpgradeSO = levelupChosen;
            levelUpButtons[i].levelupDescription.text = levelupChosen.upgradeDescription;
            levelUpButtons[i].levelupName.text = levelupChosen.upgradeName;
        }
    }

    public void ApplyUpgrade(LevelUpUpgradeSO levelupSO)
    {
        for (int i = 0; i < levelupSO.statsToIncrease.Count; i++)
        {
            for (int j = 0; j < PlayerStats.instance.statsList.Count; j++)
            {
                if (PlayerStats.instance.statsList[j].GetStatType() ==
                    levelupSO.statsToIncrease[i].GetStatType())
                {
                    PlayerStats.instance.statsList[j].Add(levelupSO.statsToIncrease[i].GetValue());
                }
            }
        }

        for (int i = 0; i < levelupSO.statsToDecrease.Count; i++)
        {
            for (int j = 0; j < PlayerStats.instance.statsList.Count; j++)
            {
                if (PlayerStats.instance.statsList[j].GetStatType() ==
                    levelupSO.statsToDecrease[i].GetStatType())
                {
                    PlayerStats.instance.statsList[j].Subtract(levelupSO.statsToDecrease[i].GetValue());
                }
            }
        }

        for (int i = 0; i < levelupSO.specialEffects.Count; i++)
        {
            levelupSO.specialEffects[i].ApplyEffect();
        }
    }

    public void UpgradeChosen()
    {
        duplicateLvlupChoice.Clear();
        pickMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void RerollChoices()
    {
        rerollAmount--;
        rerollsLeftText.text = rerollAmount.ToString();
        duplicateLvlupChoice.Clear();
        ChooseUpgradeButtons();
    }

    public void RemoveUpgradeFromList(LevelUpUpgradeSO upgradeToRemove)
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
