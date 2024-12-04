using System.Collections;
using UnityEngine;

public class PlayerEquipSystem : MonoBehaviour
{
    public static PlayerEquipSystem instance;
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
            //add sfx to show that they can use it rn
        }
    }

    IEnumerator PodCDCoroutine()
    {
        canUsePod = false;
        yield return new WaitForSeconds(equippedPodSO.podBaseCD);
        canUsePod = true;
    }
}
