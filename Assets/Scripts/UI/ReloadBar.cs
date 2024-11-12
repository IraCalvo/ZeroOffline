using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadBar : MonoBehaviour
{
    public static ReloadBar instance;
    [SerializeField] GameObject reloadBar;
    [SerializeField] GameObject loadBar;
    [SerializeField] Transform leftSide;
    [SerializeField] Transform rightSide;

    void Awake()
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

    public void ShowReloadProgress(float time)
    {
        StartCoroutine(ReloadProgressCoroutine(time));
    }

    IEnumerator ReloadProgressCoroutine(float time)
    {
        reloadBar.SetActive(true);
        loadBar.SetActive(true);
        loadBar.transform.position = leftSide.position;

        float timePassed = 0f;

        while (timePassed < time)
        {
            float t = timePassed / time;
            loadBar.transform.position = Vector3.Lerp(leftSide.position, rightSide.position, t);
            timePassed += Time.deltaTime;

            yield return null;
        }

        reloadBar.SetActive(false);
        loadBar.SetActive(false);
    }
}
