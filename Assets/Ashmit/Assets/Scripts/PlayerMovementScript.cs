using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;
    public float mouseHorizontalInput;
    public float mouseVerticalInput;

    [SerializeField]public float speed;
    [SerializeField]public float jumpForce;
    [SerializeField]public float rotationSpeed;
    [SerializeField]public float distToGround;

    public Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TEST SCENE LOADER
        if(Input.GetButtonDown("Jump") && IsGrounded())
        {
            //To be Updated
            rb.AddForce(0,jumpForce, 0, ForceMode.Impulse);
        }

        InputControl();

        Vector3 forward = transform.forward*verticalInput;
        Vector3 right = transform.right*horizontalInput;
        transform.Translate((forward + right)*speed *  Time.deltaTime);


        //transform.Rotate(0,rotationSpeed*mouseHorizontalInput,0);

        
        
    }

    void InputControl()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        mouseHorizontalInput = Input.GetAxis("Mouse X");
        mouseVerticalInput = Input.GetAxis("Mouse Y");
    }

    //@@@@@@@@@@@ TO be Updated
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.6f);
    }
}
