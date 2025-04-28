using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [Header("Pickup Settings")]
    public Transform grabPoint;           // Where the cube attaches when picked up
    public float pickupRange = 3f;        // Distance to detect cubes
    public LayerMask cubeLayer;           // Only detect cubes

    [Header("Input")]
    public KeyCode pickupKey = KeyCode.E;

    private GameObject heldCube;
    private Rigidbody heldRb;
    private FixedJoint pickupJoint;
    
    void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldCube == null)
                TryPickup();
            else
                DropCube();
        }
    }

    void TryPickup()
    {
        // Raycast from center chest/head area
        Ray ray = new Ray(transform.position + new Vector3(0,0.2f,0), transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * pickupRange, Color.green, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, cubeLayer))
        {
            heldCube = hit.collider.gameObject;
            heldRb = heldCube.GetComponent<Rigidbody>();

            if (heldRb != null)
            {
                heldRb.useGravity = false;
                heldRb.isKinematic = true;

                heldCube.transform.SetParent(grabPoint);
                heldCube.transform.localPosition = Vector3.zero;
                heldCube.transform.localRotation = Quaternion.identity;
            }
        }
    }

    void DropCube()
    {
        if (heldCube != null)
        {
            heldCube.transform.SetParent(null);
            heldRb.useGravity = true;
            heldRb.isKinematic = false;

            heldCube = null;
            heldRb = null;
        }
    }
}
