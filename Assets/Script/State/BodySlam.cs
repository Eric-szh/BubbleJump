using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySlam : State
{


    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("KeLeBodySlam");
        
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        GetComponent<KeLeBehaviour>().AttackPlayer();
        
    }

    public void Leave()
    {
        GetComponent<KeLeStateMachine>().ChangeState<DecideState>();
    }
}
