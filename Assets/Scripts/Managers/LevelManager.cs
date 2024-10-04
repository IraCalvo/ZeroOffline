using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelType
{ 
    MotherShip,
    Survival
}

public enum LevelLocation
{ 
    MotherShip,
    Planet1
}

public enum LevelDifficulty
{ 
    MotherShip,
    OneStar
}

public class LevelManager : MonoBehaviour
{
    LevelManager instance;
    public LevelType type;

    private void Awake()
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
}
