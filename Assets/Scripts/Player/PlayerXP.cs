using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    public float currentXP;
    public int currentLevel;
    public List<float> xpAmountNeededPerLevel;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("XP"))
        {
            //sfx for XP
            ObjectPoolManager.Instance.DeactivateObjectInPool(collision.gameObject);
            GainXP();
        }
    }

    public void GainXP()
    {
        currentXP++;
        if (currentXP >= xpAmountNeededPerLevel[currentLevel - 1])
        {
            currentXP = 0;
            currentLevel++;
            LevelupProcedures();
        }

        PlayerUIManager.instance.UpdateXPBar(currentXP, xpAmountNeededPerLevel[currentLevel - 1]);
    }

    public void LevelupProcedures()
    {
        PlayerUIManager.instance.levelNumber.text = currentLevel.ToString();
        //PlayerUIManager.instance.levelupPickMenu.gameObject.SetActive(true);
    }
}
