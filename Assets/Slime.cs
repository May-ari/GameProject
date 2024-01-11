using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float damage = 1f;
    public float knockbackForce = 15f;
    
    void OnCollisionEnter2D(Collision2D col)
    {
        Collider2D collider = col.collider;
        IDamageable damageable = collider.GetComponent<IDamageable>();

        if (damageable != null )
        {
            Vector3 parentPosition = transform.position;
            Vector2 direction = (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;
            
            damageable.OnHit(damage, knockback);
        }
    }
}
