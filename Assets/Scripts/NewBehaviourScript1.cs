using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollowCamera : MonoBehaviour
{
    public Transform cameraTransform; // Assign your Cinemachine Virtual Camera's transform (not the brain)

    [Header("Rotation Settings")]
    public float rotationSpeed = 200f;

    void Update()
    {
        RotatePlayerWithCamera();
    }

    private void RotatePlayerWithCamera()
    {
        // Get the camera's forward direction
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0; // Keep only the horizontal direction
        camForward.Normalize();

        if (camForward.magnitude > 0)
        {
            // Smoothly rotate the player towards the camera's forward
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
