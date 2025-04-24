using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private PlayerMovement playerMovement;


    [Header("Animation Movement Parameters")]
    public float smoothingSpeed = 10f;

    private Vector2 currentVelocity;
    private Vector2 smoothedVelocity;

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();

    }

    void Update()
    {
        // Get player movement in local space
        Vector3 localVelocity = transform.InverseTransformDirection(controller.velocity);
        currentVelocity = new Vector2(localVelocity.x, localVelocity.z);

        // Smooth movement for nice blending
        smoothedVelocity = Vector2.Lerp(smoothedVelocity, currentVelocity.normalized, Time.deltaTime * smoothingSpeed);

        // Apply to Animator
        animator.SetFloat("VelocityX", smoothedVelocity.x);
        animator.SetFloat("VelocityY", smoothedVelocity.y);


        // Handle crouch animation layer
        if (playerMovement.IsCrouching())
        {
            float targetWeight = playerMovement.IsCrouching() ? 1f : 0f;
            float currentWeight = animator.GetLayerWeight(1);
            animator.SetLayerWeight(1, Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * 10f));

        }
        else
        {
            float targetWeight = playerMovement.IsCrouching() ? 1f : 0f;
            float currentWeight = animator.GetLayerWeight(1);
            animator.SetLayerWeight(1, Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * 10f));

        }

    }
}
