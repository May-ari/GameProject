using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        RUN,
        ATTACK
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
                }
            }
        }
    }
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public ContactFilter2D movementFilter;

    Vector2 movementInput = Vector2.zero;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;
    PlayerStates currentState;

    bool stateLock = false;
    bool canMove = true;
    
    List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        if (movementInput != Vector2.zero)
        {
            bool success = TryMove(movementInput);

            if (!success)
            {
                success = TryMove(new Vector2(movementInput.x, 0));    
            }
            if(!success)
            {
                success = TryMove(new Vector2(0, movementInput.y));
            }
           // animator.SetBool("isMoving", success);
        } 
        //else
        //{
        //    animator.SetBool("isMoving", false);
        //}

        
    }

    private bool TryMove(Vector2 direction)
    {
        if (direction != Vector2.zero)
        {
        int count = rb.Cast(
                direction, 
                movementFilter, 
                castCollisions, 
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

            if (count == 0) 
                {
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
                return true;
                } else {
                return false;
                }
        } else {
          return false;
            }
        }
    
    void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
        //Update Animator for sprite direction
        if (canMove && movementInput != Vector2.zero)
        {
            CurrentState = PlayerStates.RUN;
            animator.SetFloat("xMove", movementInput.x);
            animator.SetFloat("yMove", movementInput.y);
        
        //Set facing left or right
        if (movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        }else{
            CurrentState = PlayerStates.IDLE;
        }
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
