using UnityEngine;
using System.Collections;



[RequireComponent(typeof(Animator))]
public class PlayerAnimatorController : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private PlayerMovement playerMovement;
    public CameraScript cameraScript;


    [Header("Animation Movement Parameters")]
    public float smoothingSpeed = 10f;

    private Vector2 currentVelocity;
    private Vector2 smoothedVelocity;

    [Header("Falling")]
    private bool wasGrounded = true;
    private float highestYPosDuringFall;
    private bool isFalling = false;
    public float fallImpactThreshold = 5f;
    public float fallStartThreshold = 1f;

    private bool isRecoveringFromFall = false; // NEW

    void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        // Only allow movement if not recovering
        if (!isRecoveringFromFall)
        {
            AnimateMovement();
        }

        HandleFallAnimations();
    }

    void AnimateMovement()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(controller.velocity);
        currentVelocity = new Vector2(localVelocity.x, localVelocity.z);

        smoothedVelocity = Vector2.Lerp(smoothedVelocity, currentVelocity.normalized, Time.deltaTime * smoothingSpeed);

        animator.SetFloat("VelocityX", smoothedVelocity.x);
        animator.SetFloat("VelocityY", smoothedVelocity.y);

        HandleCrouchLayer();
    }

    void HandleFallAnimations()
    {
        if (!controller.isGrounded)
        {
            if (wasGrounded)
            {
                wasGrounded = false;
                highestYPosDuringFall = transform.position.y;
            }
            else
            {
                float fallDistance = highestYPosDuringFall - transform.position.y;

                if (fallDistance > fallStartThreshold && !isFalling)
                {
                    isFalling = true;
                    animator.SetBool("IsFalling", true);
                }
            }
        }
        else
        {
            if (!wasGrounded)
            {
                float fallDistance = highestYPosDuringFall - transform.position.y;

                if (fallDistance > fallImpactThreshold)
                {
                    animator.SetTrigger("FallImpact");
                    StartRecovery(); // <<< Disable movement here
                }

                if (isFalling)
                {
                    animator.SetBool("IsFalling", false);
                    isFalling = false;
                }

                wasGrounded = true;
            }
        }
    }

    void HandleCrouchLayer()
    {
        float targetWeight = playerMovement.IsCrouching() ? 1f : 0f;
        float currentWeight = animator.GetLayerWeight(1);
        animator.SetLayerWeight(1, Mathf.Lerp(currentWeight, targetWeight, Time.deltaTime * 10f));
    }

    void StartRecovery()
    {
        isRecoveringFromFall = true;
        playerMovement.CanPlayerMove(false);
        cameraScript.CanMouseMove(false);
        //StartCoroutine(RecoverAfterDelay(11.0f)); // Assume 1 second animation length
    }

    // This will be called via animation event
    public void EndRecovery()
    {
        isRecoveringFromFall = false;
        playerMovement.CanPlayerMove(true);
        cameraScript.CanMouseMove(true);
    }

    

}
