using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 5f;
    public float smoothMovement = 10f;
    public Transform grabPoint; // Position where grabbed object attaches
    public float grabRange = 2f; // Distance to grab an object
    public LayerMask grabbableLayer; // Layer for grabbable objects

    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    private Rigidbody grabbedObject;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // Prevent unwanted rotation
    }

    void Update()
    {
        InputControl();

        // Grab / Release when pressing 'E'
        // if (Input.GetKeyDown(KeyCode.E))
        // {
        //     if (grabbedObject == null)
        //     {
        //         TryGrabObject();
        //     }
        //     else
        //     {
        //         ReleaseObject();
        //     }
        // }
    }

    void FixedUpdate()
    {
        MovePlayer();
        Jump();

        if (grabbedObject != null)
        {
            HoldObject();
        }
    }

    void InputControl()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void MovePlayer()
    {
        Vector3 moveDirection = (transform.forward * verticalInput + transform.right * horizontalInput).normalized;

        // Check the ground's normal to align movement with slopes
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1f))
        {
            moveDirection = Vector3.ProjectOnPlane(moveDirection, hit.normal); // Align with slope
        }

        Vector3 targetVelocity = moveDirection * speed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);    
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.6f);
    }

    void TryGrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, grabRange, grabbableLayer))
        {
            Rigidbody objectRb = hit.collider.GetComponent<Rigidbody>();
            if (objectRb != null)
            {
                grabbedObject = objectRb;
                grabbedObject.useGravity = false;
                grabbedObject.isKinematic = true;
            }
        }
    }

    void HoldObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.transform.position = grabPoint.position;
            grabbedObject.transform.rotation = grabPoint.rotation;
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.useGravity = true;
            grabbedObject.isKinematic = false;
            grabbedObject = null;
        }
    }
}
