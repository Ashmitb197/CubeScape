using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    float rotationOnX = 0f;
    float mouseSensitivity = 90f;
    public Transform player;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -90f, 90f);
        transform.localRotation = Quaternion.Euler(rotationOnX, 0f, 0f);

        if (player != null)
        {
            // Apply rotation using Quaternion
            player.rotation *= Quaternion.Euler(0f, mouseX, 0f);
            Debug.Log("Player Rotating: " + mouseX);
        }
        else
        {
            Debug.LogWarning("Player transform is not assigned!");
        }
    }
}
