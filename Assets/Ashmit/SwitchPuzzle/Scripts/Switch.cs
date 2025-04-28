using UnityEngine;

public class Switch : MonoBehaviour, IInteractable
{
    public string switchID = "A";
    public SwitchPuzzleManager puzzleManager;
    public GameObject interactionUI;
    public Animator anim;

    [Header("Letter Emission Setup")]
    public GameObject runicStone;
    public Renderer letterRenderer; // Assign the letter's renderer here
    public Color offColor = Color.white;
    public Color onColor = Color.green;
    public string emissionProperty = "_Emission"; // default Unity emission property

    void Start()
    {
        puzzleManager = FindObjectOfType<SwitchPuzzleManager>();    
        anim = GetComponent<Animator>();
        if (interactionUI != null)
            interactionUI.SetActive(false);

        letterRenderer = runicStone.GetComponent<Renderer>();

        SetLetterEmission(offColor); // Start with OFF color
    }

    public void Interact()
    {
        anim.SetBool("Open", true);
        SetLetterEmission(onColor); // When switch is activated, set letter to ON color
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
        SetLetterEmission(offColor); // Reset emission when turned off
    }

    public bool IsOn()
    {
        return anim.GetBool("Open");
    }

    private void SetLetterEmission(Color color)
    {
        Debug.Log("SetLetterEmission called");

        if (letterRenderer != null)
        {
            Debug.Log("Yeahhhhh - Renderer is valid!");
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



}
