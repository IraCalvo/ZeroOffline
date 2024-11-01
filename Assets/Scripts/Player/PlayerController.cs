using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float playerMS;
    Vector2 playerMovement;
    Rigidbody2D rb;
    [HideInInspector] public bool canAttack;
    float attackCD;
    PlayerDash playerDash;
    [HideInInspector] public bool playerCanBeHit;
    bool isAttacking = false;

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
        playerDash = GetComponent<PlayerDash>();
    }

    private void Update()
    {
        if (attackCD > 0)
        {
            attackCD -= Time.deltaTime;
        }
        else
        {
            canAttack = true;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMove(InputValue value)
    {
        playerMovement = value.Get<Vector2>().normalized;
    }

    public void OnFire1(InputValue value)
    {
        if (canAttack)
        {
            if (value.isPressed)
            {
                ActiveWeapon.Instance.Attack();
                canAttack = false;
            }
        }
    }

    public void OnFire2()
    { 
    
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

    void MovePlayer()
    {
        rb.velocity = playerMovement * playerMS;
    }

    public void SetAttackCD(float attackCD)
    {
        this.attackCD = attackCD;
    }
}
