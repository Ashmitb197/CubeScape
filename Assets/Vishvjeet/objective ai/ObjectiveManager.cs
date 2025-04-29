using UnityEngine;

public class ObjectiveManager : MonoBehaviour
{
    public static ObjectiveManager Instance;

    public string currentObjective = "Find the key to open the door!";
    private bool aiIsActive = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdateObjective(string newObjective)
    {
        currentObjective = newObjective;
    }

    public string GetCurrentObjective()
    {
        return currentObjective;
    }

    public void SetAIActive(bool isActive)
    {
        aiIsActive = isActive;
    }

    public bool IsAIActive()
    {
        return aiIsActive;
    }
}
