using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   private Vector3 _offset;
   [SerializeField]private Transform target;
   [SerializeField]private float smoothTime;
   private Vector3 _currentVelocity = Vector3.zero;
   // float rotationOnX;
   // float mouseSensitivity = 90f;

   private void Awake()
   {
       _offset = transform.position - target.position;

   }

   //  void Start()
   //  {
   //      Cursor.visible = false;
   //      Cursor.lockState = CursorLockMode.Locked;
   //  }

   private void LateUpdate()
   {
      Vector3 targetPosition = target.position + _offset;
      transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _currentVelocity, smoothTime);

   }
   
   // void Update()
   // {
      
   //    {
   //      // Taking mouse input
   //      float mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;
   //      float mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;

   //      // Rotate camera up and down
   //      rotationOnX -= mouseY;
   //      rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90f);
   //      transform.localEulerAngles = new Vector3(rotationOnX, 0f, 0f);

   //      // Rotate player left and right
   //      target.Rotate(Vector3.up * mouseX);
   //  }
   // }



}
