using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour , IDamageable
{
    public float StartHealth;

    protected float health;
    protected bool death;

    public event System.Action OnDeath;
   protected virtual void Start ()
    {
        health = StartHealth;
    }
        public void takehit(float damage , RaycastHit hit )
    {

        takedamage(damage);
    }

    public void takedamage(float damage)
    {
        health -= damage;

        if (health <= 0 && !death)
        {

            die();
        }
    }

    
    protected void die()
    {
        death = true;
        if (OnDeath != null )
        {
            OnDeath();
        }
        
        GameObject.Destroy(gameObject);
    }
}
