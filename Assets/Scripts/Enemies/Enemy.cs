using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEntity
{
    public bool active = true;
    public int health = 100;

    public virtual void TakeDamage(int damage)
    {
        if(!active)
        {   
            return;
        }
        
        if(health - damage <= 0)
        {
            Die();
            return;
        }

        health -= damage;

        Debug.Log("Dummy health: " + health);
    }

    public void Die()
    {
        Debug.Log("Dummy died!");
        health = 100;
    }
}
