using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] string _interactDescription;
    [SerializeField] Collider2D _collider;
    public Material _originalMaterial;
    public Material _whiteHighlightMaterial;

    public string InteractDescription
    {
        get { return _interactDescription; }
        set { _interactDescription = value; }
    }

    public Collider2D InteractableCollider
    {
        get { return _collider; }
        set { _collider = value; }
    }

    public Material OriginalMaterial
    {
        get { return GetComponent<SpriteRenderer>().material; }
        set { _originalMaterial = value; }
    }

    public Material WhiteHighlightMaterial
    {
        get { return _whiteHighlightMaterial; }
        set { _whiteHighlightMaterial = value; }
    }

    public abstract void Interact();
    public abstract void OnTriggerEnter2D(Collider2D otherCollider);
    public abstract void OnTriggerExit2D(Collider2D otherCollider);
}
