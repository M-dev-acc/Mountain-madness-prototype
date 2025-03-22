using UnityEngine;

public class FPSController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Movement Settings")]
    public float hopHeight = 3f;      // Lower height for controlled hops
    public float moveDistance = 1.2f; // Grid-based movement
    public float hopCooldown = 0.3f;  // Cooldown to prevent mid-air hopping

    private bool canHop = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb == null)
        {
            Debug.LogError("Rigidbody not found! Ensure the character has a Rigidbody.");
            return;
        }

        rb.useGravity = true;
        rb.isKinematic = false;
        rb.freezeRotation = true;

        // Increase gravity slightly for faster landing
        rb.mass = 2f;  // Increase mass to make gravity more effective
    }

    void Update()
    {
        if (canHop && IsGrounded() && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0))
        {
            Hop();
        }
    }

    void Hop()
    {
        float moveX = Mathf.Round(Input.GetAxisRaw("Horizontal")) * moveDistance;
        float moveZ = Mathf.Round(Input.GetAxisRaw("Vertical")) * moveDistance;

        Vector3 hopVelocity = new Vector3(moveX, hopHeight, moveZ); 

        rb.velocity = hopVelocity; // Directly set velocity to prevent force stacking

        canHop = false;
        Invoke(nameof(ResetHop), hopCooldown);
    }

    void ResetHop()
    {
        canHop = true;
    }

    bool IsGrounded()
    {
        // More accurate ground check with small buffer distance
        return Physics.Raycast(transform.position, Vector3.down, 1.2f);
    }
}
