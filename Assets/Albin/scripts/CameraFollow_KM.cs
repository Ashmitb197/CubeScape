using UnityEngine;

public class CameraFollow_KM : MonoBehaviour
{
    public Transform player;
    public Transform springArmComp; // Spring arm acting as a pivot for the camera
    public float mouseSensitivity = 100f;
    public float followSpeed = 10f;
    public float rotationSmoothTime = 0.1f; // Smoothness for rotation
    public Vector3 cameraOffset = new Vector3(0f, 2f, -5f);
    
    private float yRotation = 0f;
    private float xRotation = 0f;
    private Rigidbody playerRigidbody;
    private Quaternion targetRotation;
    private Quaternion targetSpringArmRotation;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        playerRigidbody = player.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        
        // Adjust rotations
        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -15f, 45f);

        // Compute target rotation
        targetSpringArmRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        if (playerRigidbody.linearVelocity.magnitude >= 0.1f)
        {
            targetRotation = Quaternion.Euler(0f, yRotation, 0f);
        }
        
        // Apply smooth rotation
        springArmComp.rotation = Quaternion.Slerp(springArmComp.rotation, targetSpringArmRotation, rotationSmoothTime);
        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, rotationSmoothTime);
        
        // Position camera smoothly
        Vector3 targetPosition = springArmComp.position + springArmComp.TransformDirection(cameraOffset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        transform.LookAt(springArmComp.position);
    }
}
