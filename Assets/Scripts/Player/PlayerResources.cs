using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    private void Awake()
    {
        if (!PlayerPrefs.HasKey("PlayerCopperAmount"))
        {
            PlayerPrefs.SetInt("PlayerCopperAmount", 0);
        }

        if (!PlayerPrefs.HasKey("PlayerIronAmount"))
        { 
            PlayerPrefs.SetInt("PlayerIronAmount", 0);
        }

        if (!PlayerPrefs.HasKey("PlayerSilverAmount"))
        {
            PlayerPrefs.SetInt("PlayerSilverAmount", 0);
        }

        if (!PlayerPrefs.HasKey("PlayerGoldAmount"))
        {
            PlayerPrefs.SetInt("PlayerGoldAmount",0);
        }

        if (!PlayerPrefs.HasKey("PlayerDiamondAmount"))
        {
            PlayerPrefs.SetInt("PLayerDiamondAmount", 0);
        }
    }
}
