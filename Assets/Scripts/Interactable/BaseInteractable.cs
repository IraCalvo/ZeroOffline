using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteractable : Interactable, IInteractable
{
    SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public override void Interact()
    {
        
    }

    public override void OnTriggerEnter2D(Collider2D otherCollider)
    {

        if (otherCollider.CompareTag("Player"))
        {
            PlayerInteract.instance.interablesInRange.Add(this);
            if (PlayerInteract.instance.GetClosestInteractable() == this)
            {
                sr.material = _whiteHighlightMaterial;
            }
        }
    }

    public override void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            sr.material = _originalMaterial;
            PlayerInteract.instance.interablesInRange.Remove(this);
            if (PlayerInteract.instance.interablesInRange.Count > 0)
            {
                PlayerInteract.instance.GetClosestInteractable().GetComponent<SpriteRenderer>().material = _whiteHighlightMaterial;
            }
        }
    }
}
