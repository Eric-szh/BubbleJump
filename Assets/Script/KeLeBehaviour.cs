using UnityEngine;

public class KeLeBehaviour : MonoBehaviour
{
    public GameObject Player;
    public const float speed = 4f;
    public int facingDirection = 0;
    public GameObject attackPoint;
    public float pushForce = 50f;
    public int contactDamage = 1;
    public Vector3 Initial_position= new Vector3(0, 0, 0);
    public float radius = 5f;
    public float attackRange = 1f;
    public float chaseRange = 5f;


    public int Health = 5;
    public int maxHealth = 5;

    private bool isDead = false;
    private bool movingRight = true;
    private Vector3 patrolTarget;

    public GameObject gameController;


    private void Start()
    {
        patrolTarget = Initial_position + new Vector3(radius, 0, 0);
    }

    private void Update()
    {
        if (isDead) return;

        GameObject player = GetComponent<PlayerStateMachine>().Player;
        if (Vector3.Distance(transform.position, player.transform.position) <= chaseRange)
        {
            AttackPlayer(player);
        }
        else
        {
            Patrol();
        }
    }
    private void Patrol()
    {
        if (movingRight)
        {
            MoveTo(patrolTarget);
            if (Vector3.Distance(transform.position, patrolTarget) < 0.1f)
            {
                movingRight = false;
                patrolTarget = Initial_position - new Vector3(radius, 0, 0);
                SetFacingLeft();
            }
        }
        else
        {
            MoveTo(patrolTarget);
            if (Vector3.Distance(transform.position, patrolTarget) < 0.1f)
            {
                movingRight = true;
                patrolTarget = Initial_position + new Vector3(radius, 0, 0);
                SetFacingRight();
            }
        }
    }

    private void AttackPlayer()
    {
        GameObject player = GetComponent<PlayerStateMachine>().Player;
        TurnToPlayer();
        if (Vector3.Distance(transform.position, player.transform.position) >= attackRange)
        {
            MoveTo(player.transform.position);
        }
        else 
        {
            PushPlayerAway(transform.position);
        }
        
    }

    public void TurnToPlayer()
    {
        GameObject player = GetComponent<PlayerStateMachine>().Player;
        if (player.transform.position.x < this.transform.position.x)
        {
            this.GetComponent<KeLeBehavior>().SetFacingLeft();
        }
        else
        {
            this.GetComponent<KeLeBehavior>().SetFacingRight();
        }
    }

    public void MoveTo(Vector3 pos, float speed = speed)
    {
        Vector3 newPos = new Vector3(pos.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, speed * Time.deltaTime);
    }

    public void PushPlayerAway(Vector3 orignalPoint, float upForce = 1.2f, float awayForce = 0.5f)
    {
        GameObject player = GetComponent<PlayerStateMachine>().Player;
        Vector3 pushDir = (player.transform.position - orignalPoint).normalized;
        Vector2 pushDir2 = new Vector2(Math.Sign(pushDir.x) * awayForce, upForce);
        if (!player.GetComponent<PlayerBehavior>().death)
        {
            player.GetComponent<Rigidbody2D>().velocity = pushDir2 * pushForce;
        }
    }

    private void AtkLeft()
    {
        this.attackPoint.transform.localPosition = new Vector3(Math.Abs(this.attackPoint.transform.localPosition.x) * -1, this.attackPoint.transform.localPosition.y, this.attackPoint.transform.localPosition.z);
    }

    private void AtkRight()
    {
        this.attackPoint.transform.localPosition = new Vector3(Math.Abs(this.attackPoint.transform.localPosition.x), this.attackPoint.transform.localPosition.y, this.attackPoint.transform.localPosition.z);
    }

    public void SetFacingLeft()
    {
        if (this.facingDirection == 1)
        {
            this.AtkLeft();
            this.facingDirection = -1;
        }
    }

    public void SetFacingRight()
    {
        if (this.facingDirection == -1)
        {
            this.AtkRight();
            this.facingDirection = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.PushPlayerAway(collision.contacts[0].point);
            collision.gameObject.GetComponent<PlayerBehavior>().TakeDamage(contactDamage);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
        {
            return;
        }

        this.Health -= damage;
        Debug.Log("KeLe took " + damage + " damage");
        
        if (this.Health <= 0)
        {
            this.GetComponent<KeLeStateMachine>().ChangeState<DeathState>();
            isDead = true;
        }
        
        }

    }

    
}
