using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float playerMS;
    Vector2 playerMovement;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    public void OnMove(InputValue value)
    {
        playerMovement = value.Get<Vector2>().normalized;
    }

    public void OnFire1()
    {
        ActiveWeapon.Instance.Attack();
    }

    public void OnFire2()
    { 
    
    }

    void MovePlayer()
    {
        rb.velocity = playerMovement * playerMS;
    }
}
