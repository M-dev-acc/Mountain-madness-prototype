using System.Collections;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public float rotationSpeed = 200f;
    public float hopDistance = 1.3f;
    public float hopHeight = 1.3f;
    public float hopDuration = 0.3f;
    private bool isHopping = false;

    private Quaternion targetRotation;

    // Gravity
    private float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;

    // Stamina
    public float maxStamina = 100f;
    public float stamina;
    public float staminaDrain = 5f;
    public float staminaRegen = 5f;
    public float criticalStaminaLevel = 35f;
    private bool canHop = true;

    void Start()
    {
        stamina = maxStamina;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative to stick to ground
        }

        HandleInput();
        SmoothRotate();

        ApplyGravity();
        RegenerateStamina();
    }

    // ----------------------
    // INPUT
    // ----------------------

    void HandleInput()
    {
        if (isHopping) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            TurnLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            TurnRight();
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded && canHop)
            StartCoroutine(BunnyHop());
    }

    // ----------------------
    // TURNING
    // ----------------------

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

    // ----------------------
    // BUNNY HOP
    // ----------------------

    IEnumerator BunnyHop()
{
    if (stamina < staminaDrain)
    {
        canHop = false;
        yield break;
    }

    isHopping = true;
    stamina -= staminaDrain;

    float elapsed = 0f;
    float initialYVelocity = Mathf.Sqrt(2 * hopHeight * -gravity); // Standard physics formula
    Vector3 hopDirection = transform.forward.normalized * hopDistance;

    Vector3 horizontalStart = transform.position;
    Vector3 horizontalEnd = horizontalStart + hopDirection;

    while (elapsed < hopDuration)
    {
        float t = elapsed / hopDuration;

        // Smooth horizontal movement from start to end
        Vector3 horizontalMove = Vector3.Lerp(horizontalStart, horizontalEnd, t);
        // Smooth vertical arc using a sine wave
        float verticalOffset = Mathf.Sin(t * Mathf.PI) * hopHeight;

        Vector3 nextPosition = horizontalMove + Vector3.up * verticalOffset;

        // Move to next position relative to current position
        Vector3 moveDelta = nextPosition - transform.position;
        controller.Move(moveDelta);

        elapsed += Time.deltaTime;
        yield return null;
    }

    isHopping = false;

    if (stamina < staminaDrain)
        canHop = false;
}


    // ----------------------
    // GRAVITY
    // ----------------------

    void ApplyGravity()
    {
        if (!isHopping)
        {
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    // ----------------------
    // STAMINA
    // ----------------------

    void RegenerateStamina()
    {
        if (!isHopping && stamina < maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
            if (stamina >= staminaDrain)
                canHop = true;

            if (stamina > maxStamina)
                stamina = maxStamina;
        }

        if (stamina <= criticalStaminaLevel)
            Debug.Log("Stamina is draining — get some grass you dummy!!!");
    }
}
