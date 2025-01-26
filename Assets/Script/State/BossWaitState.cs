using UnityEngine;

public class BossWaitState : State
{
    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    public void Activate()
    {
        GetComponent<StateMachine>().ChangeState(typeof(DecideState));
    }
}
