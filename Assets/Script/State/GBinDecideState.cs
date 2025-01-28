using UnityEngine;

public class GBinDecideState : State
{
    bool placed = false;

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        Debug.Log(placed);
        if (placed)
        {
            Debug.Log("Lob");
            GetComponent<StateMachine>().ChangeState(typeof(LobState));
        }
        else
        {
            Debug.Log("Place");
            GetComponent<StateMachine>().ChangeState(typeof(PlaceState));
            placed = true;

        }
    }

    public void Reset()
    {
        placed = false;
    }
}
