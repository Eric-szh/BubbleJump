using UnityEngine;

public class PlaceState : State
{
    public Transform left;
    public Transform right;
    public GameObject paperWall;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Place()
    {
        GameObject wall = Instantiate(paperWall, left.position, Quaternion.identity);
        GameObject wall2 = Instantiate(paperWall, right.position, Quaternion.identity);
    }

    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("Gbin_place");
    }

    public override void Tick()
    {

    }

    public override void Exit()
    {

    }

    public void LeavePlace()
    {
        GetComponent<StateMachine>().ChangeState(typeof(GBinDecideState));
    }

}
