using UnityEngine;

public class CollisonData : MonoBehaviour
{
    [SerializeField]public string tagName;
    public bool InsideTrigger;
    

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagName)) // Make sure the Player has this tag
        {
            InsideTrigger = true; // Set flag to true when player enters
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagName))
        {
            InsideTrigger = false; // Reset when player exits
        }
    }

    // void OnTriggerStay(Collider other)
    // {

    // }
}
