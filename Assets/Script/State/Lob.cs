using UnityEngine;

public class BinLob : State
{
    public GameObject BinBullet;
    private GameObject player;
    Vector3 offset = new Vector3(0.5f, 2.0f, 0.0f);
    bool justInitialized = true;
    public GameObject startPointObject;
    public override void Enter()
    {
        GetComponent<MonsterUtil>().StopMoving();
        GetComponent<AniController>().ChangeAnimationState("GBinAttack");
        

    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
        if (justInitialized)
        {
            
            justInitialized = false;
        }
    }


    public void Lob()
    {
        // Create a new GameObject to serve as the startPoint with the desired offset
        
       

        // Instantiate the BinBullet at the startPoint position
        GameObject bullet = Instantiate(BinBullet, startPointObject.transform.position, Quaternion.identity);
        bullet.GetComponent<ProjectileLob>().startPoint = startPointObject.transform;
        bullet.GetComponent<ProjectileLob>().targetPoint = GetComponent<MonsterUtil>().player.transform;
        bullet.GetComponent<ProjectileLob>().LaunchProjectile();

    }

    public void LobLeave()
    {
        _stateMachine.ChangeState(typeof(PatrolState));
    }

}
