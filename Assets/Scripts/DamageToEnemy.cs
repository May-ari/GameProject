using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageToEnemy : MonoBehaviour
{
    public float maxHealth;
    public float Health;
    void Start()
    {
        Health = maxHealth;
    }
    public void DealDamage(float Damage)
    {
        Health -= Damage;
        CheckDeath();
    }
    
    private void CheckOverHeal()
    {
        if(Health > maxHealth)
        {
            Health = maxHealth;
        }
    }
    private void CheckDeath()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
