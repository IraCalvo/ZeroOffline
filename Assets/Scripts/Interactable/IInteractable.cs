using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface IInteractable
{
    Collider2D InteractableCollider { get; }
    string InteractDescription { get; }
    Material OriginalMaterial { get; }
    Material WhiteHighlightMaterial { get; }
    TextMeshPro InteractText { get; }

    void Interact();
    void OnTriggerEnter2D(Collider2D otherCollider);
    void OnTriggerExit2D(Collider2D otherCollider);
}
