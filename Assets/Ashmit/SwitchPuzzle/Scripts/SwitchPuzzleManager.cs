using System.Collections.Generic;
using UnityEngine;

public class SwitchPuzzleManager : MonoBehaviour
{
    public List<Switch> switches = new List<Switch>();
    public Animator gateAnimator; // Reference to the Animator for the gate

    void Start()
    {
        // Automatically find all switches in the scene
        switches = new List<Switch>(FindObjectsOfType<Switch>());
        
        // Make sure we have a reference to the gate's Animator
        if (gateAnimator == null)
        {
            Debug.LogError("Gate Animator is not assigned!");
        }
    }

    public void ReceiveInput(string switchID)
    {
        // Player interacted with a switch, check if all switches are activated
        if (AllSwitchesActivated())
        {
            Debug.Log("Puzzle Solved! All switches activated.");
            
            // Trigger the animator to open the gate
            OpenGate();
        }
    }

    private bool AllSwitchesActivated()
    {
        foreach (var sw in switches)
        {
            if (!sw.IsOn()) // Assuming IsOn checks if the switch is activated
                return false;
        }
        return true;
    }

    private void OpenGate()
    {
        // Set the 'OpenGate' bool to true in the gate's Animator
        if (gateAnimator != null)
        {
            gateAnimator.SetBool("OpenGate", true);
        }
        else
        {
            Debug.LogWarning("Gate Animator is missing, can't open the gate.");
        }
    }
}
