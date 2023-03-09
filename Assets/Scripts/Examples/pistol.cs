using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistol : Gun, IInteractable
{
    [Header("Shooting")]
    public int damage;
    public float shootMaximumDistance = 100;
    public LayerMask raycastLayerMask;

    [Header("Decoration")]
    public GameObject gunFeedbackFireDecal;
    public float decalInGameTime = .1f;

    public override void Shoot()
    {
        Debug.Log("Shoot!");
        inCooldown = true;
        // ----
        StartCoroutine(ShowGunFeedbackFire());

        Ray ray = new Ray(player.plrLook.transform.position, player.plrLook.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, shootMaximumDistance, raycastLayerMask))
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

    public override void Interact(playerBrain plr)
    {
        plr.Equip(this);
        player = plr;

        GetComponent<Rigidbody>().isKinematic = true;
    }

    IEnumerator ShowGunFeedbackFire()
    {
        gunFeedbackFireDecal.SetActive(true);
        yield return new WaitForSeconds(decalInGameTime);
        gunFeedbackFireDecal.SetActive(false);
    }
}
