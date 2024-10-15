using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;
    public List<Interactable> interablesInRange;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }
    }

    public Interactable GetClosestInteractable()
    {
        Interactable closestInteractable = null;
        float closestDistance = Mathf.Infinity;

        foreach(Interactable interactable in interablesInRange)
        {
            float distance = Vector2.Distance(transform.position, interactable.transform.position);

            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestInteractable = interactable;
            }
        }

        return closestInteractable;
    }
}
