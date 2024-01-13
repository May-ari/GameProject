using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Slime : MonoBehaviour
{
    [SerializeField] GameObject GameOverScreen;
    public float damage = 1f;   
    public float knockbackForce = 15f;
    public float moveSpeed = 100f;

    public DetectionZone detectionZone;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Collider2D detectedObject0 = detectionZone.detectedObjs[0];

        if (detectedObject0)
        {
            Vector2 direction = (detectedObject0.transform.position - transform.position).normalized;
            rb.AddForce (direction * moveSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (col.gameObject.CompareTag("Player"))
        {
            GameOverScreen.SetActive(true);
        }

        if (damageable != null )
        {
            Vector3 parentPosition = transform.position;
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            
            damageable.OnHit(damage, knockback);
        }
    }
}
