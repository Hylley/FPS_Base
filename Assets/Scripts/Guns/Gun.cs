using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gun : MonoBehaviour, IInteractable
{
    public bool active;

    public float cooldown = 3;
    [HideInInspector]public bool inCooldown;

    [HideInInspector]public playerBrain player;

    public virtual void Shoot()
    {
        inCooldown = true;
        Debug.Log("Shoot!");

        StartCoroutine(WaitCooldown());
    }

    public void Interact(playerBrain plr)
    {
        GetComponent<Rigidbody>().isKinematic = true;
        plr.Equip(this);
        player = plr;
    }

    public IEnumerator WaitCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        inCooldown = false;
    }
}
