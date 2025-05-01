using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementController : MonoBehaviour
{
    [Header("Player Settings")]
    public float rotationSpeed = 200f;
    public float hopDistance = 1f;
    public float hopHeight = 0.5f;
    public float hopDuration = 0.3f;
    private bool isHopping = false;

    private Quaternion targetRotation;

    [Header("Camera Settings")]
    public Transform cameraTransform; // 🎥 Assign your Cinemachine Virtual Camera's transform here
    public float cameraRotationSpeed = 5f; // For smoother camera rotation

    void Start()
    {
        targetRotation = transform.rotation;
    }

    void Update()
    {
        HandleInput();
        SmoothRotate();
    }

    void HandleInput()
    {
        if (isHopping) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            TurnLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            TurnRight();
    }

    // -----------------------
    // 🌀 TURNING MECHANICS
    // -----------------------

    public void TurnLeft()
    {
        targetRotation *= Quaternion.Euler(0, -90, 0);
        RotateCamera(-90);
    }

    public void TurnRight()
    {
        targetRotation *= Quaternion.Euler(0, 90, 0);
        RotateCamera(90);
    }

    void SmoothRotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // -----------------------
    // 🎥 CAMERA ROTATION
    // -----------------------

    private void RotateCamera(float angle)
    {
        if (cameraTransform == null) return;

        // Rotate the camera around the player
        Vector3 pivot = transform.position;
        cameraTransform.RotateAround(pivot, Vector3.up, angle);

        // Optionally, re-align camera's local rotation to look at the player smoothly
        Quaternion targetCamRotation = Quaternion.LookRotation(transform.position - cameraTransform.position);
        cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetCamRotation, cameraRotationSpeed * Time.deltaTime);
    }
}
