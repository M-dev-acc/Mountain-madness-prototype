using Cinemachine;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement Instance { get; private set; }

    public CharacterController controller;
    public CinemachineFreeLook camera;

    public float rotationSpeed = 200f;
    public float hopDistance = 1.3f;
    public float hopHeight = 1.3f;
    public float hopDuration = 0.3f;

    public AnimationCurve hopCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

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

    private HealthManager healthManager;
    private Animator animator;

    void Awake()
    {
        Instance = this;
        cachedTransform = transform;
    }

    void Start()
    {
        healthManager = HealthManager.Instance;
        targetRotation = cachedTransform.rotation;
        animator = GetComponentInChildren<Animator>();

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
    }

    void FixedUpdate()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0f)
            velocity.y = -2f;

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
        else if (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && healthManager.DecreaseStamina(healthManager.staminaDrain))
        {
            StartHop();
            // animator.SetTrigger("Hop"); // Optional: trigger hop animation
        }
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
    // Hop System with Smooth Curve
    // -----------------------------
    void StartHop()
    {
        isHopping = true;
        hopTimer = 0f;

        hopStartPos = SnapToGrid(cachedTransform.position);
        hopEndPos = SnapToGrid(hopStartPos + cachedTransform.forward * hopDistance);
    }

    void HandleHop()
    {
        hopTimer += Time.fixedDeltaTime;
        float t = Mathf.Clamp01(hopTimer / hopDuration);

        Vector3 horizontal = Vector3.Lerp(hopStartPos, hopEndPos, t);
        float verticalOffset = hopCurve.Evaluate(t) * hopHeight;
        Vector3 targetPos = horizontal + Vector3.up * verticalOffset;

        Vector3 move = targetPos - cachedTransform.position;
        controller.Move(move);

        if (t >= 1f)
        {
            isHopping = false;
            controller.Move(hopEndPos - cachedTransform.position); // Final snap
            // animator.SetTrigger("Land"); // Optional: trigger land animation
        }
    }

    // -----------------------------
    // Grid Snapping
    // -----------------------------
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
    // Public Controls
    // -----------------------------
    public void SetMovement(bool isEnabled)
    {
        canMove = isEnabled;
    }
}
