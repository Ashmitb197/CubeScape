using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;
    // public float mouseHorizontalInput;
    // public float mouseVerticalInput;

    [SerializeField]public float walkSpeed = 3f;
    [SerializeField]public float sprintSpeed = 6f;
    [SerializeField]public float rotationSpeed;

    private Animator animator;

    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TEST SCENE LOADER
        if(Input.GetButtonDown("Jump"))
        {
            SceneController.instance.NextLevel();
        }

        InputControl();

        // Determine movement speed based on sprinting or walking
        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        Vector3 forward = transform.TransformDirection(Vector3.forward) * verticalInput;
        Vector3 right = transform.TransformDirection(Vector3.right) * horizontalInput;
        transform.Translate((forward + right) * currentSpeed * Time.deltaTime);

        //transform.Rotate(0,rotationSpeed*mouseHorizontalInput,0);

        //Animations
        if (horizontalInput == 0f && verticalInput == 0f)
        {
            //idle
            animator.SetFloat("speed", 0);
        }
        else if (!Input.GetKey(KeyCode.LeftShift)) 
        {
            //walk
            animator.SetFloat("speed", 0.5f);
        }
        else
        {
            //run
            animator.SetFloat("speed", 1);
        }

    }

    void InputControl()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // mouseHorizontalInput = Input.GetAxis("Mouse X");
        // mouseVerticalInput = Input.GetAxis("Mouse Y");
    }
}
