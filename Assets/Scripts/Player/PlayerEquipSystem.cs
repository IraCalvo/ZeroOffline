using UnityEngine;

public class PlayerEquipSystem : MonoBehaviour
{
    public static PlayerEquipSystem instance;

    void Awkake()
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
