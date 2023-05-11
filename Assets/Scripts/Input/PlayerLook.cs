using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Transform body;
    public Transform arm;

    private float xRotation = 0f;
    public float xSensitivity = 20f;
    public float ySensitivity = 20f;

    void Start() {
        // lock cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Look(Vector2 input) {
        float mouseX = input.x;
        float mouseY = input.y;

        // calculate rotation for up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // rotate the arm (hand)
        arm.transform.localRotation =  Quaternion.Euler(xRotation, 0f, 0f);

        // rotate player body
        body.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
