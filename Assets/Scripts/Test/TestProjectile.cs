using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5f;
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if (collision.GetComponent<DamageToEnemy>() != null)
            {
                collision.GetComponent<DamageToEnemy>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
   private void FixedUpdate()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
