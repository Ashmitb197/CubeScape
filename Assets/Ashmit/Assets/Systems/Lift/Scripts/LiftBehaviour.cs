using UnityEngine;

public class LiftBehaviour : MonoBehaviour
{
    [SerializeField]public Animator LiftAnimator;
    public bool IsLiftButtonPressed;
    public CollisonData platformCollisionData;
    public CollisonData BatteryCollisionData;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LiftAnimator = this.GetComponent<Animator>();
        IsLiftButtonPressed = false;
    }

    void Update()
    {
        if(Input.GetButtonDown("Interact Action") && platformCollisionData.InsideTrigger)
        {
            if(BatteryCollisionData)
            {
                if(BatteryCollisionData.InsideTrigger)
                {
                    Debug.Log("E Pressed");
                    IsLiftButtonPressed = !IsLiftButtonPressed;
                }
                else
                {
                    Debug.Log("<color=red>Insert Battery</color>");
                    
                }
            }
            else
            {
                Debug.Log("E Pressed");
                IsLiftButtonPressed = !IsLiftButtonPressed;
            }
        }

        if(BatteryCollisionData)
        {
            if(!BatteryCollisionData.InsideTrigger)
            {
                LiftAnimator.SetBool("LiftButtonPressed", false);
            }
        }


        
        LiftAnimator.SetBool("LiftButtonPressed", IsLiftButtonPressed);
    }


}
