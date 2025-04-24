using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public string switchID = "A";
    public SwitchPuzzleManager puzzleManager;
    public GameObject interactionUI;
    public Animator anim;

    void Start()
    {
        puzzleManager = FindObjectOfType<SwitchPuzzleManager>();    
        anim = GetComponent<Animator>();
        if (interactionUI != null)
            interactionUI.SetActive(false);
    }

    public void Interact()
    {
        anim.SetBool("Open", true);
        puzzleManager.ReceiveInput(switchID);
        ShowUI(false);
    }

    public void ShowUI(bool show)
    {
        if (interactionUI != null)
            interactionUI.SetActive(show);
    }

    public void TurnOffSwitch()
    {
        anim.SetBool("Open", false);
    }
}