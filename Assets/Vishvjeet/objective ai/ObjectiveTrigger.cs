using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    [TextArea]
    public string newObjectiveText = "New Objective: Find the Vault Key!";
    private string previousObjective;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Store the current objective before updating
            previousObjective = ObjectiveManager.Instance.GetCurrentObjective();
            // Update the objective to the new objective text
            ObjectiveManager.Instance.UpdateObjective(newObjectiveText);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Restore the previous objective when the player exits the trigger zone
            ObjectiveManager.Instance.UpdateObjective(previousObjective);
        }
    }
}
