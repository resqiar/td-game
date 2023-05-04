using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
    }

    public void Move(Vector2 input) {
        // init direction vector
        Vector3 direction = Vector3.zero;

        // bind input to vector direction
        direction.x = input.x;
        direction.z = input.y;

        controller.Move(transform.TransformDirection(direction) * speed * Time.deltaTime);

        // apply gravity
        velocity.y += gravity * Time.deltaTime;

        if(isGrounded && velocity.y < 0) velocity.y = -2f;

        controller.Move(velocity * Time.deltaTime);
    }

    public void Jump(){
        if (isGrounded) {
            velocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }
}
