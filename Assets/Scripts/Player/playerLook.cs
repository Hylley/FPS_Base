using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerLook : MonoBehaviour
{
    public playerBrain brain;

    public float sensibility;
    public Vector3 cameraOffset = new Vector3(0, 1, 0);
    float xRot;
    float yRot;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        yRot += Input.GetAxisRaw("Mouse X") * sensibility * .1f;
        xRot -= Input.GetAxisRaw("Mouse Y") * sensibility * .1f;

        xRot = Mathf.Clamp(xRot, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRot, yRot, 0);
        brain.plrMove.transform.rotation = Quaternion.Euler(0, yRot, 0);

        transform.position = brain.plrMove.transform.position + cameraOffset;
    }
}
