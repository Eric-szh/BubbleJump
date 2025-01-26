using UnityEngine;

public class BossLobState : State
{
    public GameObject lobProjectilePrefab;
    public Transform startPoint;    

    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("Boss_chip");
        GetComponent<MonsterUtil>().StopMoving();
        GetComponent<MonsterUtil>().TurnToPlayer();
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {

    }

    public void Lob()
    {
        
        // create a lob projectile
        GameObject lobProjectile = Instantiate(lobProjectilePrefab, startPoint.position, Quaternion.identity);
        lobProjectile.GetComponent<ProjectileLob>().startPoint = startPoint;
        lobProjectile.GetComponent<ProjectileLob>().targetPoint = GetComponent<MonsterUtil>().player.transform;
        lobProjectile.GetComponent<ProjectileLob>().LaunchProjectile();
    }

    public void ExitLob()
    {
        GetComponent<StateMachine>().ChangeState(typeof(BossDecideState));
    }
}
