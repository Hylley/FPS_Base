using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy : Enemy, IEntity
{
    public override void TakeDamage(int damage)
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
        GetComponent<AudioSource>().Play();
    }
}
