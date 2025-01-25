using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : State
{
    private GameObject player;
    public float chaseSpeed = 5f;
    public override void Enter()
    {
        Debug.Log("Chase");
        GetComponent<AniController>().ChangeAnimationState("KeLeWalk");
        
    }

    public override void Exit()
    {

    }

    public override void Tick()
    {
        player = GetComponent<KeLeStateMachine>().Player;
        GetComponent<KeLeBehaviour>().MoveTo(player.transform.position, chaseSpeed );

        string d = GetComponent<KeLeBehaviour>().Decide();
        if (d == "Patrol")
        {
            
            Leave();
        }
        else if (d == "BodySlam")
        {
            GetComponent<AniController>().ChangeAnimationState("BodySlam");
            GetComponent<KeLeStateMachine>().ChangeState<BodySlam>();
        }
       
    }

    public void Leave()
    {
        GetComponent<KeLeStateMachine>().ChangeState<DecideState>();
    }
}
