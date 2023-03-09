using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class playerMove : MonoBehaviour
{
    public playerBrain brain;

    [Header("Base Values")]
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpForce;


    [Header("Keybinds")]
    public KeyCode sprintKey = KeyCode.LeftControl;
    public KeyCode jumpKey = KeyCode.Space;

    [Header("Ground Detection")]
    public Transform foot;
    public float groundMinimumDistance = .2f;
    public LayerMask groundLayerMask;
    bool isGrounded = true;

    //Private
    Rigidbody rb;
    Vector2 xMov;
    Vector2 zMov;
    Vector2 direction;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() { MoveHandler(); }
    void Update() { JumpHandler(); }

    void MoveHandler()
    {
        xMov = new Vector2(Input.GetAxis("Horizontal") * transform.right.x, Input.GetAxis("Horizontal") * transform.right.z);
        zMov = new Vector2(Input.GetAxis("Vertical") * transform.forward.x, Input.GetAxis("Vertical") * transform.forward.z);
        
        if(Input.GetKey(sprintKey))
        {
            direction = (xMov + zMov) * sprintSpeed;
        }
        else
        {
            direction = (xMov + zMov) * walkSpeed;
        }

        rb.velocity = new Vector3(direction.x, rb.velocity.y, direction.y);
        rb.angularVelocity = new Vector3(0, 0 ,0);
    }

    void JumpHandler()
    {
        isGrounded = Physics.CheckSphere(foot.position, groundMinimumDistance, groundLayerMask);

        if(Input.GetKeyDown(jumpKey) & isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
