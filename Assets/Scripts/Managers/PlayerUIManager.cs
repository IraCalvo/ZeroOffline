using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager instance;
    [SerializeField] TextMeshProUGUI ammoCount;
    [SerializeField] TextMeshProUGUI hpAmountText;

    [SerializeField] Image hpBarFill;
    [SerializeField] Slider hpSlider;

    [SerializeField] Slider xpSlider;
    private float minXPSliderVal = 0.085f;
    private float maxXPSliderVal = 0.95f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }
    }

    public void SetupBatleUI()
    {
        
    }

    public void UpdateAmmoCount(int currentAmmo)
    {
        ammoCount.text = currentAmmo.ToString();
    }

    public void UpdateHPBar(int currentHP, int maxHP)
    { 
        hpAmountText.text = currentHP.ToString() + " / " + maxHP.ToString();
        hpSlider.value = (float)currentHP / (float)maxHP;
    }

    public void UpdateXPBar(float currentXP, float maxXPToLevelUp)
    {
        xpSlider.value = currentXP / maxXPToLevelUp;
        if (xpSlider.value > maxXPSliderVal)
        {
            xpSlider.value = minXPSliderVal;
        }
    }
}
