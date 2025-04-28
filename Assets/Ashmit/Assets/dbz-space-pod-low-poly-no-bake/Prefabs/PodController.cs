using UnityEngine;

public class PodController : MonoBehaviour
{
    [SerializeField] private GameObject players; // Camera for player 2
    [SerializeField] private Camera spaceshipCamera; // Camera that should be activated when both players are inside the trigger

    private Animator anim;

    private int playersInTrigger = 0; // Track how many players are inside the trigger

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }
    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to a player (both players have the "Player" tag)
        if (other.CompareTag("Player"))
        {
            playersInTrigger++;

            // If both players are in the trigger, change cameras
            if (playersInTrigger == 2)
            {
                ActivateSpaceshipCamera();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to a player (both players have the "Player" tag)
        if (other.CompareTag("Player"))
        {
            playersInTrigger--;

            // If one of the players leaves the trigger, revert the camera change
            if (playersInTrigger < 2)
            {
                DeactivateSpaceshipCamera();
            }
        }
    }

    private void ActivateSpaceshipCamera()
    {
        // Disable current player cameras
        players.gameObject.SetActive(false);
        anim.SetTrigger("CloseTrigger");

        // Enable spaceship camera
        spaceshipCamera.gameObject.SetActive(true);
    }

    private void DeactivateSpaceshipCamera()
    {
        // Disable spaceship camera
        spaceshipCamera.gameObject.SetActive(false);
    }
}
