using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float crouchSpeed = 2f;
    private float currentSpeed;

    [Header("Crouch")]
    public float crouchHeight = 7.2f;
    public float standHeight = 14.4f;
    private bool isCrouching = false;
    public float SpringArmYValue;

    public CharacterController controller;
    public CapsuleCollider capsuleCollider;

    [Header("Inputs")]
    public string inputTypeVertical;
    public string inputTypeHorizontal;
    public string inputTypeCrouch;

    void Start()
    {
        capsuleCollider = this.GetComponent<CapsuleCollider>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        HandleCrouch();
    }

    void Move()
    {
        float x = Input.GetAxis(inputTypeHorizontal);
        float z = Input.GetAxis(inputTypeVertical);
        Vector3 move = transform.right * x + transform.forward * z;
        move.Normalize(); // Optional: prevents faster diagonal movement

        currentSpeed = isCrouching ? crouchSpeed : walkSpeed;

        // Apply movement with gravity
        controller.SimpleMove(move * currentSpeed);
    }

    void HandleCrouch()
    {
        if (Input.GetButtonDown(inputTypeCrouch))
        {
            isCrouching = !isCrouching;
            if (isCrouching)
            {
                capsuleCollider.enabled = false;
                controller.height = crouchHeight;
            }
            else
            {
                capsuleCollider.enabled = true;
                controller.height = standHeight;
            }

            controller.center = new Vector3(0, controller.height / 2f, 0);
        }
    }

    public bool IsCrouching()
    {
        return isCrouching;
    }
}
