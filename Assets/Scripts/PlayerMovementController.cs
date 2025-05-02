using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public float rotationSpeed = 200f;
    public float hopDistance = 1f;
    public float hopHeight = 0.5f;
    public float hopDuration = 0.3f;
    private bool isHopping = false;

    private Quaternion targetRotation;

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
        else if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(BunnyHop());
    }

    // -----------------------
    // 🌀 TURNING MECHANICS
    // -----------------------

    public void TurnLeft()
    {
        targetRotation *= Quaternion.Euler(0, -90, 0);
    }

    public void TurnRight()
    {
        targetRotation *= Quaternion.Euler(0, 90, 0);
    }

    void SmoothRotate()
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    // -----------------------
    // 🐰 BUNNY HOP MECHANIC
    // -----------------------

    IEnumerator BunnyHop()
    {
        isHopping = true;

        Vector3 startPos = transform.position;
        Vector3 direction = transform.forward;
        Vector3 endPos = startPos + direction * hopDistance;

        float elapsedTime = 0f;

        while (elapsedTime < hopDuration)
        {
            float t = elapsedTime / hopDuration;
            float height = Mathf.Sin(Mathf.PI * t) * hopHeight; // parabolic arc
            transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        isHopping = false;
    }
}
