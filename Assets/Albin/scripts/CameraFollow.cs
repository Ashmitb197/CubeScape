using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // Player
    [SerializeField] private float smoothTime = 0.3f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Vector3 offset = new Vector3(0, 5, -5); // Default offset
    
    private Vector3 _currentVelocity = Vector3.zero;
    private float _rotationY = 0f;
    
    private void Awake()
    {
        _rotationY = target.eulerAngles.y;
    }

    private void LateUpdate()
    {
        FollowTarget();
        //RotatePlayerWithMouse();
    }

    private void FollowTarget()
    {
        Vector3 targetPosition = target.position + Quaternion.Euler(0, _rotationY, 0) * offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);
        transform.LookAt(target.position); // Keep looking at player
    }

   //  private void RotatePlayerWithMouse()
   //  {
   //      if (Input.GetMouseButton(1)) // Right-click to rotate
   //      {
   //          float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
   //          _rotationY += mouseX;
   //          transform.rotation = Quaternion.Euler(0, _rotationY, 0); // Rotate player
   //      }
   //  }
}
