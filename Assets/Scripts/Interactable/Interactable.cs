using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] string _interactDescription;
    [SerializeField] Collider2D _collider;
    public Material _originalMaterial;
    public Material _whiteHighlightMaterial;
    public SpriteRenderer sr;
    public TextMeshPro _interactionText;

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

    public TextMeshPro InteractText
    {
        get { return _interactionText; }
        set { _interactionText = value; }
    }

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public virtual void Interact()
    {
        print("Description: " + _interactDescription);
    }

    public virtual void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            PlayerInteract.instance.interablesInRange.Add(this);
            if (PlayerInteract.instance.GetClosestInteractable() == this)
            {
                sr.material = _whiteHighlightMaterial;
                _interactionText.gameObject.SetActive(true);
            }
        }
    }
    public virtual void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            sr.material = _originalMaterial;
            PlayerInteract.instance.interablesInRange.Remove(this);
            _interactionText.gameObject.SetActive(false);
            if (PlayerInteract.instance.interablesInRange.Count > 0)
            {
                PlayerInteract.instance.GetClosestInteractable().GetComponent<SpriteRenderer>().material = _whiteHighlightMaterial;
                PlayerInteract.instance.GetClosestInteractable()._interactionText.gameObject.SetActive(true);
            }
        }
    }
}
