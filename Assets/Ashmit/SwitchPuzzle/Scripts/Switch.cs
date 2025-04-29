using UnityEngine;

public class Switch : MonoBehaviour
{
    public string switchID = "A";
    public SwitchPuzzleManager puzzleManager;
    public Animator anim;

    [Header("Letter Emission Setup")]
    public GameObject runicStone;
    public Renderer letterRenderer; // Assign the letter's renderer here
    public Color offColor = Color.white;
    public Color onColor = Color.green;
    public string emissionProperty = "_Emission"; // default Unity emission property

    [Header("Message Setup")]
    public string triggerMessage = "Press 'Q' (Keyboard) or 'A' (Controller) to activate the switch.";
    private StartupMessages messageManager;

    private void Start()
    {
        puzzleManager = FindObjectOfType<SwitchPuzzleManager>();    
        messageManager = FindObjectOfType<StartupMessages>();
        anim = GetComponent<Animator>();
        
        if (runicStone != null)
            letterRenderer = runicStone.GetComponent<Renderer>();

        SetLetterEmission(offColor); // Start with OFF color
    }

    public void Interact()
    {
        anim.SetBool("Open", true);
        SetLetterEmission(onColor); // When switch is activated, set letter to ON color
        puzzleManager.ReceiveInput(switchID);
    }

    public void TurnOffSwitch()
    {
        anim.SetBool("Open", false);
        SetLetterEmission(offColor); // Reset emission when turned off
    }

    public bool IsOn()
    {
        return anim.GetBool("Open");
    }

    private void SetLetterEmission(Color color)
    {
        if (letterRenderer != null)
        {
            Material mat = letterRenderer.material;
            mat.SetColor("_EmissionColor", color); 
            mat.EnableKeyword("_EMISSION");
            DynamicGI.SetEmissive(letterRenderer, color); 
        }
        else
        {
            Debug.LogWarning("LetterRenderer is NULL!!");
        }
    }

    // ========== Handle Player Entering/Exiting Trigger ==========

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (messageManager != null)
            {
                messageManager.OverrideMessage(triggerMessage);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (messageManager != null)
            {
                messageManager.ClearOverride();
            }
        }
    }
}
