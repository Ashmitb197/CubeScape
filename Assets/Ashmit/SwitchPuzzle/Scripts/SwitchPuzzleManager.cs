using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzleManager : MonoBehaviour
{
    public List<string> correctSequence = new List<string>();
    public List<string> playerInput = new List<string>();
    public List<Switch> switches = new List<Switch>();

    void Start()
    {
        // Automatically find all switches in the scene
        switches = new List<Switch>(FindObjectsOfType<Switch>());
    }

    public void ReceiveInput(string switchID)
    {
        playerInput.Add(switchID);
        int currentIndex = playerInput.Count - 1;

        if (playerInput[currentIndex] != correctSequence[currentIndex])
        {
            Debug.Log("Wrong switch! Resetting...");
            playerInput.Clear();

            //Turn off all switches
            foreach (var sw in switches)
            {
                sw.TurnOffSwitch();
            }

            return;
        }

        if (playerInput.Count == correctSequence.Count)
        {
            Debug.Log("Correct sequence! Puzzle solved.");
            // Trigger something (e.g., door open)
        }
    }
}
