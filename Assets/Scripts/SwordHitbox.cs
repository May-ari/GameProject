using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public float swordDamage = 1f;
    public float knockbackForce = 500f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(0.15f, -0.024f, 0);
    public Vector3 faceLeft = new Vector3(-0.15f, -0.024f, 0);
    public Vector3 faceUp = new Vector3(0.03f, 0.05f, 0);
    public Vector3 faceDown = new Vector3(0.03f, -0.1f, 0);
    void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            Vector3 parentPosition = transform.parent.position;
            Vector2 direction = (Vector2)(collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;
            
            damageableObject.OnHit(swordDamage, knockback);
        }
    } 
    void IsFacingRight(bool isFacingRight)
    {
        if(isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }
     void IsFacingUp(bool isFacingUp)
    {
        if(isFacingUp)
        {
            gameObject.transform.localPosition = faceUp;
        }
    }
    void IsFacingDown(bool isFacingDown)
    {
        if(isFacingDown)
        {
            gameObject.transform.localPosition = faceDown;
        }
    }
    
}
