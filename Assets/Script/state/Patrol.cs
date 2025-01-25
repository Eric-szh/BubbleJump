using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : State
{
   

    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("KeLeWalk");
        
    }

    public override void Exit()
    {
      
    }

    public override void Tick()
    {
        GetComponent<KeLeBehaviour>().Patrol();
        string d = GetComponent<KeLeBehaviour>().Decide();
        if (d== "Chase" ||d=="BodySlam")
        {
            GetComponent<AniController>().ChangeAnimationState("KeLeDecide");
            Leave();
        }
    }

    public void Leave()
    {
        GetComponent<KeLeStateMachine>().ChangeState<DecideState>();
    }
}
