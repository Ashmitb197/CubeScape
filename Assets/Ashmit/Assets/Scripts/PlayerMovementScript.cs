using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{

    public float horizontalInput;
    public float verticalInput;
    public float mouseHorizontalInput;
    public float mouseVerticalInput;

    [SerializeField]public float speed;
    [SerializeField]public float rotationSpeed;

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
        if(Input.GetButtonDown("Jump"))
        {
            
            SceneController.instance.NextLevel();
        }

        InputControl();

        Vector3 forward = transform.TransformDirection(Vector3.forward)*verticalInput;
        Vector3 right = transform.TransformDirection(Vector3.right)*horizontalInput;
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
}
