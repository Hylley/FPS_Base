using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : baseGun, IInteractable
{
    public int damage;
    public float shootMaximumDistance = 100;
    public Transform tip;

    public override void Shoot()
    {
        Debug.Log("Shoot!");
        inCooldown = true;
        // ----

        Ray ray = new Ray(player.plrLook.transform.position, player.plrLook.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, shootMaximumDistance))
        {
            Debug.Log("Hit: " + hit.transform);

            if(hit.transform.GetComponent<IEntity>() != null)
            {
                hit.transform.GetComponent<IEntity>().TakeDamage(damage);
                Debug.Log("Hit entity!");
            }
        }

        // ----
        StartCoroutine(WaitCooldown());
    }
}
