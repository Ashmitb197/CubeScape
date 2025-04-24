using UnityEngine;
using UnityEditor;

public class GateController : MonoBehaviour
{
    public SwitchTriggers switch1;
    public SwitchTriggers switch2;
    

    public Transform gate; // Gate object to move
    // public Vector3 closedPosition;
    // public Vector3 halfOpenPosition;
    // public Vector3 openPosition;
    public Animator anim;
    public AnimatorOverrideController gateController;

    public float speed = 2f;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.runtimeAnimatorController = gateController;
    }

    void Update()
    {   
        if(switch1 && switch2)
        {
            if (switch1.isPressed && switch2.isPressed)
            {
                anim.SetBool("OpenGate", true);
            }
            else
            {
                anim.SetBool("OpenGate", false);
            }
        }
        else
        {
            if(switch1 && switch1.isPressed)
            {
                anim.SetBool("OpenGate", true);
            }
            if(switch2 && switch2.isPressed)
            {
                anim.SetBool("OpenGate", true);
            }
            else
            {
                anim.SetBool("OpenGate", false);
            }
        }
    }
}
