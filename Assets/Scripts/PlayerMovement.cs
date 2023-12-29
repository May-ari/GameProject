using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public float speed;
   private Vector2 direction;
   private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        TakeInput();
        Move();
    }
    private void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        SetAnimatorMovement(direction);
    }
    private void TakeInput()
    {
        direction = Vector2.zero;
        if(Input.GetKey(KeyCode.W)) 
            {
            direction += Vector2.up;
            }
        if(Input.GetKey(KeyCode.A)) 
            {
            direction += Vector2.left;
            }
        if(Input.GetKey(KeyCode.S)) 
            {
            direction += Vector2.down;
            }
        if(Input.GetKey(KeyCode.D)) 
            {
            direction += Vector2.right;
            }
    }
    private void SetAnimatorMovement(Vector2 direction)
    {
        animator.SetFloat("X_Direction", direction.x);
        animator.SetFloat("Y_Direction", direction.y);
        print(animator.GetFloat("X_Direction")); //CAN BE DELETED (Just for info in editor)
        print(animator.GetFloat("Y_Direction")); //CAN BE DELETED (Just for info in editor)
    }
}
