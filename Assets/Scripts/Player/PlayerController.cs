using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerStates
{
    public bool playerCanBeHit;
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float playerMS;
    Vector2 playerMovement;
    Rigidbody2D rb;
    SpriteRenderer sr;

    PlayerDash playerDash;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
        }

        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerDash = GetComponent<PlayerDash>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
        FlipSprite();
    }

    public void OnMove(InputValue value)
    {
        playerMovement = value.Get<Vector2>().normalized;
    }

    public void OnFire1(InputValue value)
    {
        //need a way to check for holding down
        if (value.isPressed)
        {
            ActiveWeapon.Instance.Attack();
        }
    }

    public void OnFire2()
    {
        ActivePod.instance.UsePod();
    }

    public void OnReload()
    {
        ActiveWeapon.Instance.Reload();
    }

    public void OnInteract()
    {
        if (PlayerInteract.instance.interablesInRange.Count > 0)
        { 
            PlayerInteract.instance.GetClosestInteractable().Interact();
        }
    }

    public void OnDash()
    {
        playerDash.Dash();
    }

    public void OnOpenInventory()
    {
        if (!InventoryUIManager.instance.inventoryUI.activeInHierarchy)
        {
            InventoryUIManager.instance.OpenInventory();
        }
        else 
        {
            InventoryUIManager.instance.CloseInventory();
        }
    }

    public void OnEsc()
    {
        UIManager.instance.PauseGame();
    }

    void MovePlayer()
    {
        rb.linearVelocity = playerMovement * playerMS;
    }

    void FlipSprite()
    {
        if (CursorManager.instance.mouseWorldPos.x < transform.position.x)
        {
            sr.flipX = true;
        }
        else
        {
            sr.flipX = false;
        }
    }
}
