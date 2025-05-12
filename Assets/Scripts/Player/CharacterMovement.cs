using Cinemachine;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController controller;
    public CinemachineFreeLook camera;

    public float rotationSpeed = 200f;
    public float hopDistance = 1.3f;
    public float hopHeight = 1.3f;
    public float hopDuration = 0.3f;

    public float maxStamina = 100f;
    public float staminaDrain = 2f;
    public float staminaRegen = 5f;
    public float criticalStaminaLevel = 35f;

    public float stamina;
    private bool isHopping = false;
    private bool canMove = true;

    private Quaternion targetRotation;
    private Transform cachedTransform;

    // Hop movement
    private float hopTimer;
    private Vector3 hopStartPos;
    private Vector3 hopEndPos;

    // Gravity
    private Vector3 velocity;
    private float gravity = -9.81f;

    private bool isGrounded;
    private bool warnedLowStamina = false;

    public static CharacterMovement Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        cachedTransform = transform;
    }

    void Start()
    {
        stamina = maxStamina;
        targetRotation = cachedTransform.rotation;

        if (camera != null)
        {
            camera.Follow = cachedTransform;
            camera.LookAt = cachedTransform;
        }
    }

    void Update()
    {
        if (!canMove) return;

        HandleInput();
        SmoothRotate();
        // RegenerateStamina();
    }

    void FixedUpdate()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f; // small downward force to keep grounded

        if (isHopping)
        {
            HandleHop();
        }
        else
        {
            ApplyGravity();
        }
    }

    // -----------------------------
    // Input
    // -----------------------------
    void HandleInput()
    {
        if (isHopping) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
            RotateLeft();
        else if (Input.GetKeyDown(KeyCode.RightArrow))
            RotateRight();
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded && stamina >= staminaDrain)
            StartHop();
    }

    // -----------------------------
    // Rotation
    // -----------------------------
    void RotateLeft()
    {
        targetRotation *= Quaternion.Euler(0, -90, 0);
        if (camera != null) camera.m_XAxis.Value -= 90;
    }

    void RotateRight()
    {
        targetRotation *= Quaternion.Euler(0, 90, 0);
        if (camera != null) camera.m_XAxis.Value += 90;
    }

    void SmoothRotate()
    {
        cachedTransform.rotation = Quaternion.RotateTowards(
            cachedTransform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    // -----------------------------
    // Hop System with Grid Snapping
    // -----------------------------
    void StartHop()
    {
        isHopping = true;
        stamina -= staminaDrain;

        hopTimer = 0f;
        hopStartPos = SnapToGrid(cachedTransform.position); // Snaps to grid
        hopEndPos = SnapToGrid(hopStartPos + cachedTransform.forward * hopDistance); // Snaps to grid
    }

    void HandleHop()
    {
        hopTimer += Time.fixedDeltaTime;
        float t = hopTimer / hopDuration;

        if (t >= 1f)
        {
            controller.Move(hopEndPos - cachedTransform.position); // final snap
            isHopping = false;
            return;
        }

        Vector3 horizontal = Vector3.Lerp(hopStartPos, hopEndPos, t);
        float verticalOffset = Mathf.Sin(t * Mathf.PI) * hopHeight;
        Vector3 targetPos = horizontal + Vector3.up * verticalOffset;

        controller.Move(targetPos - cachedTransform.position);
    }

    // Snapping function to align with grid
    Vector3 SnapToGrid(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x),
            Mathf.Round(position.y),
            Mathf.Round(position.z)
        );
    }

    // -----------------------------
    // Gravity
    // -----------------------------
    void ApplyGravity()
    {
        velocity.y += gravity * Time.fixedDeltaTime;
        controller.Move(velocity * Time.fixedDeltaTime);
    }

    // -----------------------------
    // Stamina
    // -----------------------------
    void RegenerateStamina()
    {
        if (!isHopping && stamina < maxStamina)
        {
            stamina += staminaRegen * Time.deltaTime;
            stamina = Mathf.Min(stamina, maxStamina);
        }

        if (stamina <= criticalStaminaLevel && !warnedLowStamina)
        {
            Debug.Log("Stamina is low — rest or eat!");
            warnedLowStamina = true;
        }
        else if (stamina > criticalStaminaLevel)
        {
            warnedLowStamina = false;
        }
    }

    // -----------------------------
    // Public Controls
    // -----------------------------
    public void SetMovement(bool isEnabled)
    {
        if (!isHopping)
            canMove = isEnabled;
    }
}
