using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 4f;
    public float sprintSpeed = 7f;
    public float crouchSpeed = 2f;
    private float currentSpeed;

    [Header("Stamina")]
    public float maxStamina = 5f;
    private float currentStamina;
    public float staminaDrain = 1f;
    public float staminaRegen = 0.5f;

    [Header("Crouch")]
    public float crouchHeight = 1f;
    public float standHeight = 2f;
    private bool isCrouching = false;

    public CharacterController controller;


    [Header("Inputs")]
    public string inputTypeVertical;
    public string inputTypeHorizontal;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        currentStamina = maxStamina;
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

        // Determine speed
        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && !isCrouching)
        {
            currentSpeed = sprintSpeed;
            currentStamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            currentSpeed = isCrouching ? crouchSpeed : walkSpeed;
            currentStamina += staminaRegen * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        // Apply movement with gravity
        controller.SimpleMove(move * currentSpeed);
    }

    void HandleCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isCrouching = !isCrouching;
            controller.height = isCrouching ? crouchHeight : standHeight;
            controller.center = new Vector3(0, controller.height / 2f, 0);
        }
    }
}
