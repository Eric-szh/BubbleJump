using UnityEngine;

public class BossWaitState : State
{
    public override void Enter()
    {
        GetComponent<MonsterUtil>().SetMovingPoint(transform.position);
        GetComponent<AniController>().ChangeAnimationState("Boss_normal");
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    public void Activate()
    {
        GetComponent<StateMachine>().ChangeState(typeof(BossDecideState));
    }
}
