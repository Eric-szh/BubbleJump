using UnityEngine;

public class ThrowAndRunState : State
{
    public string animationName;
    public float detectionRange;
    public GameObject projectilePrefab;
    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState(animationName, true);
        GetComponent<MonsterUtil>().StopMoving();
        // run away from the player by first set the facing direction
        if (GetComponent<MonsterUtil>().player.transform.position.x < transform.position.x)
        {
            GetComponent<MonsterUtil>().FaceRight();
        }
        else
        {
            GetComponent<MonsterUtil>().FaceLeft();
        }
    }

    public override void Exit()
    {
    }

    public override void Tick()
    {
    }

    public void Run()
    {
        // get the player's position
        Vector3 playerPosition = GetComponent<MonsterUtil>().player.transform.position;
        // add a force to the monster to run away from the player, only move in horizontal direction
        GetComponent<Rigidbody2D>().AddForce(new Vector2((transform.position.x - playerPosition.x) * 3, 0), ForceMode2D.Impulse);
        
    }

    public void Throw()
    {
        // create a projectile and set its direction and speed
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<ProjectileStaight>().direction = GetComponent<MonsterUtil>().GetPlayerDirection();
        AudioManager.Instance.PlaySound(22, 0.5f);
    }

    public void Decide()
    {
        if (GetComponent<MonsterUtil>().GetPlayerDistance() < detectionRange)
        {
            GetComponent<StateMachine>().ChangeState(typeof(ThrowAndRunState));
        } else
        {
            GetComponent<StateMachine>().ChangeState(typeof(PatrolState));
        }
    }

}
