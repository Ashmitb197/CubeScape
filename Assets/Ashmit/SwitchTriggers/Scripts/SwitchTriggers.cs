using UnityEngine;

public class SwitchTriggers : MonoBehaviour
{
    public bool isPressed = false;
    public Renderer switchRenderer;
    public Color inactiveColor = Color.red;
    public Color activeColor = Color.green;
    private Material switchMaterial;


    void Start()
    {
        switchMaterial = switchRenderer.material;
        SetEmissionColor(inactiveColor);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("GoalCube"))
        {
            isPressed = true;
            SetEmissionColor(activeColor);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("GoalCube"))
        {
            isPressed = false;
            SetEmissionColor(inactiveColor);
        }
    }


    void SetEmissionColor(Color color)
    {
        switchMaterial.EnableKeyword("_EMISSION");
        switchMaterial.SetColor("_EmissionColor", color);
        DynamicGI.SetEmissive(switchRenderer, color); // for realtime GI
    }

}
