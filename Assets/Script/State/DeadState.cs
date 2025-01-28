using UnityEngine;

public class DeadState : State
{
    public string deadAniName;
    public bool stayDead;
    public bool killSelf = false;
    public string stayDeadAniName;
    public override void Enter()
    {
        canExit = false;
        GetComponent<AniController>().ChangeAnimationState(deadAniName);
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    public void Kill()
    {
        // set the monster to inactive
        if (killSelf) {
            gameObject.SetActive(false);
        }

        if (stayDead)
        {
            GetComponent<AniController>().ChangeAnimationState(stayDeadAniName);
        }

              
    }

    public void Reset()
    {
        canExit = true;
        GetComponent<StateMachine>().ChangeState(GetComponent<StateMachine>().startState.GetType());
    }

}
