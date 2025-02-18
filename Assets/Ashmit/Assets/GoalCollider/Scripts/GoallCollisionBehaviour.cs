using UnityEngine;

public class GoallCollisionBehaviour : MonoBehaviour
{

    public Material GoalZoneMat;

    [SerializeField]public Color red;
    [SerializeField]public Color green;


    public bool isOnGoalZone;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //goalColliderRenderer = this.GetComponent<Renderer>();
        isOnGoalZone = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    void ChangeColor()
    {
        if(isOnGoalZone)
        {
            GoalZoneMat.SetColor("_BaseColor", green);
            GoalZoneMat.SetColor("_EmissionColor", green);
        }

        else{
            GoalZoneMat.SetColor("_BaseColor", red);
            GoalZoneMat.SetColor("_EmissionColor", red);
        }
    }


    void OnCollisionStay(Collision collision)
    {
        
        if(collision.collider.CompareTag("GoalCube"))
        {
            isOnGoalZone = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        isOnGoalZone = false;
    }
}
