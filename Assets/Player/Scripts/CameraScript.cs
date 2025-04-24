using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraScript : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public Transform springArm;

    [Header("Camera Settings")]
    public float mouseSensitivity = 2.0f;
    public float rotationSmoothTime = 0.1f;
    public float armLength = 4f;
    public Vector3 cameraOffset = new Vector3(0f, 1.5f, 0f);
    public LayerMask collisionLayers;

    private Vector2 currentRotation;
    private Vector2 rotationVelocity;

    private CharacterController controller;
    private Transform camTransform;

    public string inputTypeMouseX;
    public string inputTypeMouseY;

    public float rotationSpeed = 720f; // You can tweak this for snappier or smoother turn


    [Header("Spring Arm Height Adjustment")]
    public float standingHeight = 1.5f;
    public float crouchingHeight = 0.75f;
    public float heightLerpSpeed = 5f;

    private PlayerMovement playerMovement;
    private float currentOffsetY;



    void Start()
    {

        playerMovement = player.GetComponent<PlayerMovement>();
        currentOffsetY = standingHeight;


        if(inputTypeMouseX == "Mouse X_C")
        {
            mouseSensitivity *= 10;
        }

        Cursor.lockState = CursorLockMode.Locked;
        controller = player.GetComponent<CharacterController>();
        camTransform = this.transform;

        currentRotation.x = player.eulerAngles.y;
        currentRotation.y = springArm.eulerAngles.x;
    }

    void LateUpdate()
    {
        RotateCameraAndPlayer();
        PositionCamera(); // Now runs every frame for obstacle handling

        float targetY = playerMovement.IsCrouching() ? crouchingHeight : standingHeight;
        currentOffsetY = Mathf.Lerp(currentOffsetY, targetY, Time.deltaTime * heightLerpSpeed);
        cameraOffset = new Vector3(cameraOffset.x, currentOffsetY, cameraOffset.z);

    }

    void RotateCameraAndPlayer()
    {
        float mouseX = Input.GetAxis(inputTypeMouseX) * mouseSensitivity;
        float mouseY = Input.GetAxis(inputTypeMouseY) * mouseSensitivity;

        currentRotation.x += mouseX;
        currentRotation.y -= mouseY;
        currentRotation.y = Mathf.Clamp(currentRotation.y, -40f, 80f);

        springArm.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0f);

        float playerSpeed = controller.velocity.magnitude;
        if (playerSpeed > 0.1f)
        {
            
            Quaternion targetRotation = Quaternion.Euler(0f, currentRotation.x, 0f);
            player.rotation = Quaternion.RotateTowards(player.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }
    }

    void PositionCamera()
    {
        // Set spring arm position
        springArm.position = player.position + cameraOffset;

        // Desired camera position at full arm length
        Vector3 desiredPosition = springArm.position - springArm.forward * armLength;

        // Raycast to detect obstacles
        RaycastHit hit;
        if (Physics.Raycast(springArm.position, -springArm.forward, out hit, armLength, collisionLayers))
        {
            // Move camera to hit point (a little forward to prevent clipping)
            camTransform.position = hit.point + springArm.forward * 0.3f;
        }
        else
        {
            // No obstacle, normal distance
            camTransform.position = desiredPosition;
        }

        camTransform.LookAt(springArm.position);
    }
}
