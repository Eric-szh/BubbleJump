using UnityEngine;

public class BossTrashState : State
{
    public GameObject trashcan;
    public Transform kickPoint;
    public float originalSpeed;

    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("Boss_trash");
        originalSpeed = GetComponent<MonsterUtil>().speed;
        GetComponent<MonsterUtil>().speed = 5;
        GetComponent<MonsterUtil>().MoveTo(kickPoint.position);
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {
        GetComponent<MonsterUtil>().speed = originalSpeed;
    }

    public void TrashcanFall()
    {
        trashcan.GetComponent<BossTrashControl>().Fall();
    }

    public void LeaveTrash()
    {
        GetComponent<StateMachine>().ChangeState(typeof(BossDecideState));
    }
}
