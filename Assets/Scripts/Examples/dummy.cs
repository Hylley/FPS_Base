using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : MonoBehaviour, IEntity
{
    public int health = 100;

    public void TakeDamage(int damage)
    {
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
