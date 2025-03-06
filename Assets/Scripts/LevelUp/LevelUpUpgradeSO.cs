using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpUpgrade", menuName = "Scriptable Objects/LevelUpUpgrade")]
public class LevelUpUpgradeSO : ScriptableObject
{
    public string upgradeName;
    public string upgradeDescription;
    public List<Stat> statsToIncrease;
    public List<Stat> statsToDecrease;
    public List<ILevelUpEffect> specialEffects;
}

//TODO: rarity maybe?
