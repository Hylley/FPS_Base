using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBrain : MonoBehaviour, IEntity
{
    public playerLook plrLook;
    public playerMove plrMove;
    
    [Header("Player Status")]
    public float health = 100;

    [Header("Interaction system")]
    public KeyCode interactKey = KeyCode.E;
    public float maxVisionDistance;

    [Header("Gun equipment")]
    public Gun equippedGun;
    public Transform rightHand;
    public float lerpSpeed = .1f;

    [Header("Grapple equipment")]
    public grappler equippedGrappler;
    public Transform leftHand;

    void Update()
    {
        // Player vision -----------------------------------------------------
        
        Ray ray = new Ray(plrLook.transform.position, plrLook.transform.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, maxVisionDistance))
        {
            if(hit.transform.GetComponent<IInteractable>() != null)
            {
                UIManager.instance.interactableFeddbackGUI.SetActive(true);

                if(Input.GetKeyDown(interactKey))
                {
                    hit.transform.GetComponent<IInteractable>().Interact(this);
                }
            }
            else
            {
                UIManager.instance.interactableFeddbackGUI.SetActive(false);
            }
        }

        // Shoot logic -----------------------------------------------------

        if(!equippedGun || !equippedGun.active || equippedGun.inCooldown) return;

        if(Input.GetMouseButton(0))
        {
            equippedGun.Shoot();
        }
    }

    void FixedUpdate()
    {
        if(equippedGun)
        {
            equippedGun.transform.position = Vector3.Lerp(equippedGun.transform.position, rightHand.position, lerpSpeed);
            equippedGun.transform.rotation = Quaternion.Lerp(equippedGun.transform.rotation, rightHand.rotation, lerpSpeed);
        }

        if(equippedGrappler)
        {
            equippedGrappler.transform.position = Vector3.Lerp(equippedGrappler.transform.position, leftHand.position, lerpSpeed);
        }
    }

    public void Equip(Gun newGun)
    {
        if(equippedGun) { equippedGun.active = false; }

        equippedGun = newGun;
        newGun.active = true;
    }

    public void TakeDamage(int damage)
    {
        if(health - damage <= 0)
        {
            Die();
            return;
        }

        health -= damage;
    }

    public void Die()
    {
        Debug.Log("Game Over!");
    }
}
