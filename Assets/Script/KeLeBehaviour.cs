using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeLeBehaviour : MonoBehaviour
{
    
    public GameObject player;
    public const float speed = 3f;
    public int facingDirection = 0;
    public GameObject attackPoint;
    public float pushForce = 50f;
    public int contactDamage = 1;
    public Vector3 Initial_position = new Vector3(0, 0, 0); 
    public float radius = 5f;


    public float chaseRange = 5f;
    public float attackRange = 2f;
    


    public int Health = 5;
    public int maxHealth = 5;

    public bool forced_patrol = false;
    private bool isDead = false;
    private bool movingRight = false;
    private Vector3 patrolTarget;

    public GameObject gameController;






    public void Patrol()
    {
        if (movingRight)
        {
            
            
           
            patrolTarget = Initial_position + new Vector3(radius, 0, 0);
            GetComponent<MonsterUtil>().SetMovingPoint(patrolTarget);
            if (Vector3.Distance(transform.position, patrolTarget) < 0.1f)
            {
                movingRight = false;
                
            }
        }
        else
        {
            
            patrolTarget = Initial_position - new Vector3(radius, 0, 0);
            GetComponent<MonsterUtil>().SetMovingPoint(patrolTarget);
            if (Vector3.Distance(transform.position, patrolTarget) < 0.1f)
            {
                movingRight = true;
                
                
            }
        }
    }
    public void AttackPlayer()
    {


        GetComponent< MonsterUtil > ().SetMovingPoint(player.transform.position);


    }

    public void TurnToPlayer()
    {
        
        if (player.transform.position.x < this.transform.position.x)
        {
            GetComponent<MonsterUtil>().FaceLeft();
        }
        else
        {
            GetComponent<MonsterUtil>().FaceRight();
        }
    }

    public void MoveTo(Vector3 pos, float speed = speed)
    {
        Debug.Log("moving to " + pos);
        TurnToPlayer();
        Vector3 newPos = new Vector3(pos.x, this.transform.position.y, this.transform.position.z);
        this.transform.position = Vector3.MoveTowards(this.transform.position, newPos, speed * Time.deltaTime);
    }

    public void PushPlayerAway(Vector3 orignalPoint, float upForce = 1.2f, float awayForce = 0.5f)
    {
       
        Vector3 pushDir = (player.transform.position - orignalPoint).normalized;
        Vector2 pushDir2 = new Vector2(Math.Sign(pushDir.x) * awayForce, upForce);
        if (!player.GetComponent<CharacterController2D>().isDead)
        {
            player.GetComponent<Rigidbody2D>().linearVelocity = pushDir2 * pushForce;
        }
    }

    


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.PushPlayerAway(collision.contacts[0].point);
            collision.gameObject.GetComponent<CharacterController2D>().Damage(contactDamage);
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

    public string Decide()
    {

        

        float player_distance = Vector3.Distance(player.transform.position, this.transform.position);
        float distance = Vector3.Distance(this.transform.position, Initial_position);
        if (Health <= 0)
        {
            return "Death";
        }
        else if ( distance>= radius || player_distance >= chaseRange )
        {
            Debug.Log("Patrol");
            return "Patrol";

        }
        else if (player_distance < attackRange)
        {
            Debug.Log("Attack");
            return "BodySlam";
        }

        else
        {
            Debug.Log("Chase");
            return "Chase";
        }

    }
}



