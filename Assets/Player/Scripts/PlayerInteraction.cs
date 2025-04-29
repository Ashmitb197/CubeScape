using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Switch currentSwitch;
    public string InteractionType;

    void Update()
    {
        if (currentSwitch != null && Input.GetButtonDown(InteractionType))
        {
            currentSwitch.Interact();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Switch switchComponent = other.GetComponent<Switch>();
        if (switchComponent != null)
        {
            currentSwitch = switchComponent;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Switch switchComponent = other.GetComponent<Switch>();
        if (switchComponent != null && switchComponent == currentSwitch)
        {
            currentSwitch = null;
        }
    }
}
