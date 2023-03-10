using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour, IInteractable
{
    public void Interact(playerBrain player)
    {
        Debug.Log("Medkit collected!");
        Destroy(this.gameObject);
    }
}
