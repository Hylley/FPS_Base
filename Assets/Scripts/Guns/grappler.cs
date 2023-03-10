using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappler : Gun, IInteractable
{
    public bool isGrappling;

    [Header("Shooting")]
    public float shootMaximumDistance = 100;
    public LayerMask raycastLayerMask;

    [Header("Grappling")]
    public Vector3 grapplePoint;
    public Transform tip;
    public float acumulativeForce;
    SpringJoint joint;
    LineRenderer lr;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        if(!active) return;

        if(Input.GetMouseButtonDown(1))
        {
            Shoot();
        }
        else if(Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }

        DrawRope();

        if(isGrappling)
        {
            transform.LookAt(grapplePoint);
            player.plrMove.GetComponent<Rigidbody>().AddForce(player.plrMove.transform.forward * Input.GetAxis("Vertical") * acumulativeForce, ForceMode.Acceleration);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, player.leftHand.rotation, 1);
        }
    }

    public override void Shoot()
    {
        // ----
        Ray ray = new Ray(player.plrLook.transform.position, player.plrLook.transform.forward);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, shootMaximumDistance, raycastLayerMask))
        {
            player.plrMove.GetComponent<Rigidbody>().AddForce(player.plrMove.transform.up * acumulativeForce, ForceMode.Acceleration);
            grapplePoint = hit.point;
            joint = player.plrMove.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distance = Vector3.Distance(player.plrMove.transform.position, grapplePoint);
            joint.maxDistance = distance * .1f;
            joint.minDistance = distance * .1f;

            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;

            player.plrMove.moveAllow = false;
            isGrappling = true;
        }

        // GetComponent<AudioSource>().Play();

        // ----
    }

    void StopGrapple()
    {
        player.plrMove.moveAllow = true;
        isGrappling = false;
        Destroy(joint);
        lr.positionCount = 0;
    }

    void DrawRope()
    {
        if (!isGrappling) return;

        lr.SetPosition(0, tip.position);
        lr.SetPosition(1, grapplePoint);
    }

    public override void Interact(playerBrain plr)
    {
        plr.equippedGrappler = this;
        player = plr;
        active = true;

        GetComponent<Rigidbody>().isKinematic = true;
    }
}
