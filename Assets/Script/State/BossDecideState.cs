using UnityEngine;

public class BossDecideState : State
{
    public float decideTime = 2.0f;

    public override void Enter()
    {
        Invoke("decide", decideTime);
        GetComponent<AniController>().ChangeAnimationState("Boss_normal");
    }

    public override void Exit()
    {
        CancelInvoke("decide");
    }

    public override void Tick()
    {
    }

    void decide()
    {
        // randomly choose the next state
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            GetComponent<StateMachine>().ChangeState(typeof(MeleeAttackState));
        }
        else
        {
            GetComponent<StateMachine>().ChangeState(typeof(BossLobState));
        }
    }
}
