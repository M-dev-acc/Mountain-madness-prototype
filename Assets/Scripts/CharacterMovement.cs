using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller; // Reference to the CharacterController
    public float speed = 6f; // Movement speed
    float gravity = -9.81f; 

    public float jumpHeight = 1.3f; 
    private Vector3 velocity; // Stores velocity for gravity
    private bool isGrounded; // Check if player is on the ground
    float turnSmoothVelocity;

    public float maxStamina = 100f;  // Maximum stamina
    public float stamina;            // Current stamina
    public float staminaDrain = 5f; // Stamina consumed per hop
    public float staminaRegen = 5f;  // Stamina recovered per second
    public float criticalStaminaLevel = 35f;  // Stamina recovered per second
    private bool canHop = true;      // Can the player hop?

    void Start()
    {
        stamina = maxStamina;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0f, vertical).normalized;

        if (move.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canHop)
        {
            if (stamina >= staminaDrain)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

                Vector3 forwardHop = transform.forward * 2f;
                controller.Move(forwardHop * Time.deltaTime);

                stamina -= staminaDrain;

                

                if (stamina < staminaDrain)
                {
                    canHop = false;
                }   
            }
        }

        if (stamina >= maxStamina)
        {
            stamina = maxStamina;
            canHop = true;
        }

        if (stamina == criticalStaminaLevel)
        {
            Debug.Log("Stamina is draining get some grass you dummy!!!");
        }
        Debug.Log("Stamina: " + stamina);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}
