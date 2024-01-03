using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask whatIsPlayer;

    private Transform target;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 movement;
    private Vector3 dir;

    private bool isInChaseRange;
    private bool isInAttackRange;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }
    private void Update()
    {
        anim.SetBool("isRunning", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, checkRadius, whatIsPlayer);

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        dir.Normalize();
        movement = dir;
        if (shouldRotate)
        {
            anim.SetFloat("X_Direction", dir.x);
            anim.SetFloat("Y_Direction", dir.y);
        }
    }
    private void FixedUpdate()
    {
        if (isInAttackRange && isInChaseRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            rb.velocity = Vector2.zero;
        }
    }
    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2)transform.position + (dir * speed * Time.deltaTime));
    }
    /*public UnityEvent<Vector2> OnMovementInput, OnPointerInput;
    public UnityEvent OnAttack;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float chaseDistanceThreshold, attackDistanceThreshold;

    [SerializeField]
    private float attackDelay = 1;
    private float passedTime = 1;

    private void Update()
    {
        if (player == null)
        return;
            
        float distance = Vector2.Distance(player.position, transform.position);
        if (distance < chaseDistanceThreshold)
        {
            OnPointerInput?.Invoke(player.position);
            if (distance <= attackDistanceThreshold)
            {
                OnMovementInput?.Invoke(Vector2.zero);
                if (passedTime >= attackDelay)
                {
                    passedTime = 0;
                    OnAttack?.Invoke();
                }
            }
            else
            {
                Vector2 direction = player.position - transform.position;
                OnMovementInput?.Invoke(direction.normalized);
            }
        }
        if (passedTime < attackDelay)
        {
            passedTime += Time.deltaTime;
        }
    }*/
}
