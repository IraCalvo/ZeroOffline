using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    TextMeshPro dmgTxt;
    [SerializeField] float fadeDuration;
    [SerializeField] Color normalColor;
    [SerializeField] Color critColor;

    private void Awake()
    {
        dmgTxt = GetComponent<TextMeshPro>();
    }

    private void OnDisable()
    {
        Color textColor = dmgTxt.color;
        textColor.a = 1f;
        dmgTxt.color = textColor;
    }

    public void ShowPopup(int dmgToShow, bool isCrit)
    {
        dmgTxt.text = dmgToShow.ToString();

        if (isCrit)
        {
            dmgTxt.color = critColor;
        }
        else
        { 
            dmgTxt.color = normalColor;
        }
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        float timer = fadeDuration;
        while (timer > 0)
        {
            float alphaAmount = timer / fadeDuration;
            alphaAmount = Mathf.Clamp01(alphaAmount);

            Color objectColor = dmgTxt.color;
            objectColor.a = alphaAmount;
            dmgTxt.color = objectColor;

            timer -= Time.deltaTime;
            yield return null;
        }

        ObjectPoolManager.Instance.DeactivateObjectInPool(gameObject);
    }
}
