using UnityEngine;

public class MessageTrigger : MonoBehaviour
{
    public string triggerMessage;
    private StartupMessages messageManager;

    void Start()
    {
        messageManager = FindObjectOfType<StartupMessages>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure your player has the "Player" tag
        {
            messageManager.OverrideMessage(triggerMessage);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            messageManager.ClearOverride();
        }
    }
}
