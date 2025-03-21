using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody rb;

    public float dampTime = 0.1f; // Smoothing time for transitions
    public Transform playerFrontCheck; // Empty GameObject in front of player
    public float frontCheckDistance = 1f; // Adjust based on player size
    public LayerMask detectionLayer; // Layer mask for objects to check

    private bool isPushing = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>(); // Ensure Animator is on the Player Model
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (animator == null || rb == null) return;

        UpdateAnimation();
        CheckForPushableObject();
    }

    void UpdateAnimation()
    {
        // Get movement velocity relative to the player’s direction
        Vector3 localVelocity = transform.InverseTransformDirection(rb.linearVelocity); // Fixed velocity reference

        // Set parameters for Blend Tree
        animator.SetFloat("VelocityX", localVelocity.x, dampTime, Time.deltaTime);
        animator.SetFloat("VelocityZ", localVelocity.z, dampTime, Time.deltaTime);
    }

    void CheckForPushableObject()
    {
        RaycastHit hit;
        bool detected = Physics.Raycast(transform.position + (Vector3.up*0.5f), transform.forward, out hit, frontCheckDistance, detectionLayer);

        if (detected && hit.collider.attachedRigidbody != null) // Object has Rigidbody
        {
            if (!isPushing)
            {
                isPushing = true;
                animator.SetLayerWeight(1, 1f); // Enable Push Layer
            }
        }
        else
        {
            if (isPushing)
            {
                isPushing = false;
                animator.SetLayerWeight(1, 0f); // Return to Base Layer
            }
        }
    }

    private void OnDrawGizmos()
    {
        // Draw a debug line for the front check
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerFrontCheck.position, playerFrontCheck.position + transform.forward * frontCheckDistance);
    }
}
