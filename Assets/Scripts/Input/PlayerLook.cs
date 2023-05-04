using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera camera;
    private float xRotation = 0f;
    public float xSensitivity = 20f;
    public float ySensitivity = 20f;

    public void Look(Vector2 input) {
        float mouseX = input.x;
        float mouseY = input.y;

        // calculate rotation for up and down
        xRotation -= (mouseY * Time.deltaTime) * ySensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        // rotate camera 
        camera.transform.localRotation =  Quaternion.Euler(xRotation, 0, 0);

        // rotate player left and right (not camera but player!)
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity);
    }
}
