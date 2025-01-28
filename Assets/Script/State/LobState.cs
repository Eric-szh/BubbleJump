using UnityEngine;

public class LobState : State
{
    public GameObject paperBullet;
    public Transform startPoint;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Lob()
    {
        GameObject bullet = Instantiate(paperBullet, startPoint.position, Quaternion.identity);
        bullet.GetComponent<ProjectileLob>().startPoint = startPoint;
        bullet.GetComponent<ProjectileLob>().targetPoint = GetComponent<MonsterUtil>().player.transform;
        bullet.GetComponent<ProjectileLob>().LaunchProjectile();
        AudioManager.Instance.PlaySound(29, 0.5f);
    }

    public void LeaveLob()
    {
        GetComponent<StateMachine>().ChangeState(typeof(GBinDecideState));
    }

    public override void Enter()
    {
        GetComponent<MonsterUtil>().StopMoving();
        GetComponent<AniController>().ChangeAnimationState("Gbin_throw");
    }

    public override void Tick()
    {
    }

    public override void Exit()
    {
    }
}
