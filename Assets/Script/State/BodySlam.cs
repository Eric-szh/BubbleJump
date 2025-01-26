using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodySlam : State
{
    public float slamSpeed = 3.5f;

    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("KeLeBodySlam");
        GetComponent<MonsterUtil>().speed = slamSpeed;
        GetComponent<KeLeBehaviour>().AttackPlayer();

    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        
    }

    public void Leave()
    {
        GetComponent<MonsterUtil>().speed = 1.0f;

        GetComponent<StateMachine>().ChangeState(typeof(PatrolState));
    }
}
