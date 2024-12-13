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

    public TextMeshProUGUI levelNumber;

    [SerializeField] Image podUIFill;
    [SerializeField] TextMeshProUGUI podCDTimer;

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
        if (xpSlider.value >= 1 || xpSlider.value < minXPSliderVal)
        {
            xpSlider.value = minXPSliderVal;
        }
    }

    public void StartPodCD(float time)
    {
        StartCoroutine(PodCDCoroutine(time));
    }

    IEnumerator PodCDCoroutine(float totalTime)
    {
        podUIFill.fillAmount = 1;
        float timePassed = 0f;

        while (timePassed < totalTime)
        {
            float percentage = 1f -  (timePassed / totalTime);

            podUIFill.fillAmount = percentage;
            timePassed += Time.deltaTime;
            yield return null;
        }

        podUIFill.fillAmount = 0;
    }
}
