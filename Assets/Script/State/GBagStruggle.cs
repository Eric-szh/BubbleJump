using UnityEngine;

public class GBagStruggle : State
{
    float struggleTime = 2f;
    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("GBag_Struggle");
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
        Invoke("Leave", struggleTime);
    }

    public override void Exit()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
        CancelInvoke("Leave");
    }

    public override void Tick()
    {
    }

    public void Leave()
    {
        GetComponent<StateMachine>().ChangeState(typeof(PatrolState));
    }

}
