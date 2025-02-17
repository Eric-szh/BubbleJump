using UnityEngine;

public class JumpAndLob : State
{
    public GameObject oilBullet;
    public int jumpStrength;
    public override void Enter()
    {
        GetComponent<MonsterUtil>().StopMoving();
        GetComponent<AniController>().ChangeAnimationState("Pot_jump");
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    public void Jump()
    {
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * jumpStrength, ForceMode2D.Impulse);
    }

    public void Lob()
    {
        GameObject bullet = Instantiate(oilBullet, transform.position, Quaternion.identity);
        bullet.GetComponent<ProjectileLob>().startPoint = transform;
        bullet.GetComponent<ProjectileLob>().targetPoint = GetComponent<MonsterUtil>().player.transform;
        bullet.GetComponent<ProjectileLob>().LaunchProjectile();
        AudioManager.Instance.PlaySound(25, 0.5f);
    }

    public void Leave()
    {
        _stateMachine.ChangeState(typeof(PatrolState));
    }

 }
