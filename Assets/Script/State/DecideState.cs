using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecideState : State
{
    public float chaseRange = 5f;
    public float radius = 5f;
    public Vector3 Initial_position = new Vector3(0, 0, 0);
    
    public override void Enter()
    {
        Debug.Log("Decide");
        GetComponent<AniController>().ChangeAnimationState("KeLeDecide");
        
    }

    public override void Exit()
    {
       
    }

    public override void Tick()
    {
        string decision= GetComponent<KeLeBehaviour>().Decide();
        if (decision == "Chase")
        {
            
            GetComponent<KeLeStateMachine>().ChangeState<Chase>();
        }
        else if (decision == "Patrol")
        {
           
            GetComponent<KeLeStateMachine>().ChangeState<Patrol>();
        }
        
    }

    
       
        
}

    

   

