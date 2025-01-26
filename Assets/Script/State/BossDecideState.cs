using UnityEngine;

public class BossDecideState : State
{
    public float decideTime = 2.0f;
    public bool lastSoup = false;

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
        if (lastSoup)
        {
            GetComponent<StateMachine>().ChangeState(typeof(BossTrashState));
            lastSoup = false;
            return;
        }

        int random;
        if (GetComponent<MonsterUtil>().health <= 25)
        {
             random = Random.Range(0, 3);
        } else
        {
             random = Random.Range(0, 2);
        }
        
        if (random == 0)
        {
            GetComponent<StateMachine>().ChangeState(typeof(MeleeAttackState));
        }
        else if (random == 1)
        {
            GetComponent<StateMachine>().ChangeState(typeof(BossLobState));
        }
        else
        {
            GetComponent<StateMachine>().ChangeState(typeof(BossSoupState));
            lastSoup = true;
        }
    }
}
