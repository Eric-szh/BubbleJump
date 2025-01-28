using UnityEngine;

public class BossSoupState : State
{
    public GameObject noddle1;
    public GameObject noddle2;
    public Transform kickPoint;
    public float originalSpeed;
    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("Boss_soup");
        originalSpeed = GetComponent<MonsterUtil>().speed;
        GetComponent<MonsterUtil>().speed = 5;
        GetComponent<MonsterUtil>().MoveTo(kickPoint.position);
    }

    public override void Exit()
    {
        GetComponent<MonsterUtil>().speed = originalSpeed;
    }

    public override void Tick()
    {
    }


    public void noodleFall()
    {
        Debug.Log("noodle fall");
        noddle1.GetComponent<NoodleCtrl>().Fall();
        noddle2.GetComponent<NoodleCtrl>().Fall();
    }

    public void LeaveSoup()
    {
        GetComponent<StateMachine>().ChangeState(typeof(BossDecideState));
    }
}
