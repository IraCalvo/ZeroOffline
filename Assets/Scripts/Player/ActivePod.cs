using System.Collections;
using UnityEngine;

public class ActivePod : MonoBehaviour
{
    public static ActivePod instance;
    public GameObject equippedPod;
    bool canUsePod = true;
    PodSO equippedPodSO;

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

    private void Start()
    {
        NewPod(equippedPod);
    }

    public void NewPod(GameObject newPod)
    { 
        equippedPod = newPod;
        equippedPodSO = equippedPod.GetComponent<IPod>().PodSO;
    }

    public void UsePod()
    {
        if (canUsePod)
        {
            equippedPod.GetComponent<IPod>().UsePod();
            PlayerUIManager.instance.StartPodCD(equippedPodSO.podBaseCD);
            StartCoroutine(PodCDCoroutine());
        }
        else
        { 
            //TODO: add sfx that they cannot use pod rn
        }
    }

    IEnumerator PodCDCoroutine()
    {
        canUsePod = false;
        yield return new WaitForSeconds(equippedPodSO.podBaseCD);
        canUsePod = true;
    }
}
