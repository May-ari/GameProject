using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        RUN,
        ATTACK,
        DEATH
    }

    PlayerStates CurrentState
    {
        set
    {
       
            if (stateLock == false)
            {
                currentState = value;

                switch (currentState)
                {
                    case PlayerStates.IDLE:
                        animator.Play("IDLE");
                        canMove = true;
                        break;
                                    
                    case PlayerStates.RUN:
                        animator.Play("RUN");
                        canMove = true;
                        break;

                    case PlayerStates.ATTACK:
                        animator.Play("ATTACK");
                        stateLock = true;
                        canMove = false;
                        break;

                    case PlayerStates.DEATH:
                        animator.Play("DEATH");
                        isAlive = false;
                        canMove = false;
                        break;
                    }
             }
            
        }
    }
    public float moveSpeed = 150f;
    public float maxSpeed = 2.2f;
    public float idleFriction = 0.9f;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Vector2 moveInput = Vector2.zero;

    public GameObject swordHitbox;
    
    PlayerStates currentState;

    public bool isAlive = true;
    bool stateLock = false;
    bool canMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (canMove && moveInput != Vector2.zero)
        {
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + (moveInput * moveSpeed * Time.deltaTime), maxSpeed);

            if (moveInput.x > 0)
            {  
                spriteRenderer.flipX = false; 
                gameObject.BroadcastMessage("IsFacingRight", true);
                CurrentState = PlayerStates.RUN;
            }
            else if (moveInput.x < 0)
            {  
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("IsFacingRight", false);
                CurrentState = PlayerStates.RUN;
            }
            else if (moveInput.y < 0 || moveInput.y > 0)
            {     
                CurrentState = PlayerStates.RUN;
            }
          UpdateAnimatorParameters();
        }
        
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            CurrentState = PlayerStates.IDLE;
        }
       
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); 
    }

    void UpdateAnimatorParameters()
    {
        animator.SetFloat("xMove", moveInput.x);
        animator.SetFloat("yMove", moveInput.y);
    }

    void OnFire()
    {
        CurrentState = PlayerStates.ATTACK;
    }

    void OnAttackFinished()
    {
        stateLock = false;
        CurrentState = PlayerStates.IDLE;
    }
   

}
