using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TPSController : MonoBehaviour
{
    public Transform cameraPivot; // Empty GameObject behind player (set in Inspector)
    public Transform playerModel; // Reference to the player model for rotation
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;

    public float lookSpeed = 2f;
    public float lookXLimit = 45f; // Max up/down camera movement

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    public bool canMove = true;
    public bool isOnGround = true;

    IEnumerator StartMovementAfterDelay()
    {
        yield return new WaitForSeconds(0.5f); // Wait 0.5 seconds before enabling movement
        canMove = true;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        StartCoroutine(StartMovementAfterDelay());
    }

    void Update()
    {
        Vector3 forward = cameraPivot.forward; // Get camera's forward direction
        Vector3 right = cameraPivot.right;
        forward.y = 0; // Ignore vertical tilting
        right.y = 0;

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float speed = isRunning ? runSpeed : walkSpeed;

        float moveX = Input.GetAxis("Vertical") * speed;
        float moveY = Input.GetAxis("Horizontal") * speed;

        // Movement direction based on camera angle
        Vector3 movement = (forward * moveX) + (right * moveY);
        float previousY = moveDirection.y; // Preserve falling/jumping speed
        moveDirection = movement;
        moveDirection.y = previousY;

        // Jump
        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            isOnGround = false;
            moveDirection.y = jumpPower;
        }

        if (!characterController.isGrounded)
        {
            isOnGround = true;
            moveDirection.y -= gravity * Time.deltaTime;
        }

        characterController.Move(moveDirection * Time.deltaTime);

        // Camera rotation (Mouse Look)
        if (canMove)
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = -Input.GetAxis("Mouse Y") * lookSpeed;

            rotationX = Mathf.Clamp(rotationX + mouseY, -lookXLimit, lookXLimit);
            cameraPivot.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.Rotate(Vector3.up * mouseX);
        }

        // Rotate player model to face movement direction
        if (movement.magnitude > 0)
        {
            playerModel.rotation = Quaternion.LookRotation(movement);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("On ground");
            isOnGround = true;
        }
    }
}
