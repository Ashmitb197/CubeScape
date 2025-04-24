using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable currentInteractable;
    public string InteractionType;

    void Update()
    {
        if (currentInteractable != null && Input.GetButtonDown(InteractionType))
        {
            currentInteractable.Interact();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
            currentInteractable.ShowUI(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable != null && interactable == currentInteractable)
        {
            currentInteractable.ShowUI(false);
            currentInteractable = null;
        }
    }
}
