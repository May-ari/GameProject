using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class Fireball : MonoBehaviour
{
    private GameObject shooter;
    private Vector2 direction;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float lifeTime = 10f;

    public void Launch(GameObject _shooter, Vector2 target)
    {
        shooter = _shooter;
        direction = (target - (Vector2)transform.position).normalized;
    }

    void Awake()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
    }
    
    void FixedUpdate()
    {
        if (shooter != null)
        {
            GetComponent<Rigidbody2D>().MovePosition((Vector2)transform.position + direction  * speed * Time.deltaTime);
        }

        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != shooter && collision.TryGetComponent<Health>(out Health enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
