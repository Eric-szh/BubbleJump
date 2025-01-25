using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("Death");
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {

    }

    public void Leave()
    {
        GetComponent<KeLeStateMachine>().ChangeState<DecideState>();
    }
}
