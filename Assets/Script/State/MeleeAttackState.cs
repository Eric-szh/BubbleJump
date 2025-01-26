using UnityEngine;

public class MeleeAttackState : State
{
    float originalSpeed;
    public State nextState;
    public Transform attackPoint;
    public float attackRange = 1.0f;
    public override void Enter()
    {
        GetComponent<AniController>().ChangeAnimationState("Boss_kick");
        Vector3 playerLastLocation = GetComponent<MonsterUtil>().player.transform.position;
        // depending on the player's location, the boss will kick the player to the left or right 2 units
        Vector3 direction = GetComponent<MonsterUtil>().GetPlayerDirection();
        Vector3 kickTarget = playerLastLocation + direction * -2;
        originalSpeed = GetComponent<MonsterUtil>().speed;   
        GetComponent<MonsterUtil>().speed = 3.0f;
        GetComponent<MonsterUtil>().MoveTo(kickTarget);
    }

    public override void Exit()
    {
        GetComponent<MonsterUtil>().speed = originalSpeed;
        GetComponent<MonsterUtil>().MoveTo(transform.position);
    }

    public override void Tick()
    {
    }

    public void LeaveMeele()
    {
        GetComponent<StateMachine>().ChangeState(nextState.GetType());
    }

    public void Attack()
    {
        // search for the player in the attack range
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, LayerMask.GetMask("Player"));
        if (hitPlayer.Length > 0)
        {
            // if the player is in the attack range, deal damage to the player
            print(hitPlayer[0]);
            hitPlayer[0].gameObject.GetComponent<CharacterController2D>().Damage(1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // draw a circle around the attack point to show the attack range
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
