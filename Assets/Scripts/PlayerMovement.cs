using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
   public float speed;
   private Vector2 dir;
   private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        TakeInput();
        Move();
    }
    private void Move()
    {
        dir.Normalize();
        transform.Translate(speed * Time.deltaTime * dir);
        
        if (dir.x != 0 || dir.y != 0) 
        {
           SetAnimatorMovement(dir);
        }
        else
        {
            anim.SetLayerWeight(1, 0);
        }
    }
    private void TakeInput()
    {
        dir = Vector2.zero;
        if(Input.GetKey(KeyCode.W)) 
            {
            dir += Vector2.up;
            }
        if(Input.GetKey(KeyCode.A)) 
            {
            dir += Vector2.left;
            }
        if(Input.GetKey(KeyCode.S)) 
            {
            dir += Vector2.down;
            }
        if(Input.GetKey(KeyCode.D)) 
            {
            dir += Vector2.right;
            }
    }
    private void SetAnimatorMovement(Vector2 direction)
    {
        anim.SetLayerWeight(1, 1);
        anim.SetFloat("X_Direction", dir.x);
        anim.SetFloat("Y_Direction", dir.y);
        
    }
}
