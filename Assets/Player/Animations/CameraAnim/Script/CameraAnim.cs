using UnityEngine;

public class CameraAnim : MonoBehaviour
{
    public PlayerMovement playerMovementScript;
    public PauseMenu pauseMenu;
    public Camera cameraComp;

    [Header("Pause Zoom Settings")]
    public Vector3 pauseOffset = new Vector3(0, 2, -5); // Move camera up and back
    public float transitionSpeed = 2f;

    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float originalWidth;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private float targetWidth;

    private Transform camTransform;

    void Awake()
    {
        camTransform = transform;
        originalPosition = camTransform.localPosition;
        originalRotation = camTransform.localRotation;

        if (cameraComp == null)
            cameraComp = GetComponent<Camera>();

        originalWidth = cameraComp.rect.width;
    }

    void Update()
    {
        if (pauseMenu.isPaused())
        {
            playerMovementScript.ResetMovement();
            playerMovementScript.enabled = false;

            // Set target transform
            targetPosition = originalPosition + pauseOffset;
            targetRotation = Quaternion.Euler(20, 0, 0); // looking down a bit more
            targetWidth = 1f;
        }
        else
        {
            playerMovementScript.enabled = true;

            targetPosition = originalPosition;
            targetRotation = originalRotation;
            targetWidth = originalWidth;
        }

        // Smooth camera motion and rotation
        camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, targetPosition, Time.unscaledDeltaTime * transitionSpeed);
        camTransform.localRotation = Quaternion.Slerp(camTransform.localRotation, targetRotation, Time.unscaledDeltaTime * transitionSpeed);

        // Smooth rect width transition
        Rect camRect = cameraComp.rect;
        camRect.width = Mathf.Lerp(camRect.width, targetWidth, Time.unscaledDeltaTime * transitionSpeed);
        cameraComp.rect = camRect;
    }
}
