using UnityEngine;

public class DeadState : State
{
    public string deadAniName;
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
        Destroy(gameObject);
    }

}
