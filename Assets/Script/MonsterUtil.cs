using Unity.VisualScripting;
using UnityEngine;

public class MonsterUtil : MonoBehaviour
{
    public float speed = 1.0f;
    [SerializeField]
    private Vector3 movingPoint;
    public float movingPointTolerance = 0.1f;
    public bool setFaceLeft = false;
    public bool faceLeft = false;
    public bool lastFaceLeft = true;
    public GameObject player;
    public State restrainState;
    public State dieState;
    public bool moving = true;
    public int health = 1;
    public int hitSoundIndex = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movingPoint = transform.position;
        if (setFaceLeft)
        {
            faceLeft = true;
            lastFaceLeft = true;
            FaceLeft();
        }
        else
        {
            faceLeft = false;
            lastFaceLeft = false;
            FaceRight();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Vector3.Distance(transform.position, movingPoint) > movingPointTolerance && moving)
        {
            // move the monster to the moving point
            transform.position = Vector3.MoveTowards(transform.position, movingPoint, speed * Time.deltaTime);
            // if the moving point is on the left of the monster, face left
            if (movingPoint.x < transform.position.x)
            {
                FaceLeft();
            }
            // if the moving point is on the right of the monster, face right
            else
            {
                FaceRight();
            }
        }
    }

    public void Restrain()
    {
        StopMoving();
        if (restrainState != null)
        {
            GetComponent<StateMachine>().ChangeState(restrainState.GetType());
        }
        
    }

    public float GetPlayerDistance()
    {
        // get the distance between the monster and the player
        return Vector3.Distance(transform.position, player.transform.position);
    }

    public Vector3 GetPlayerDirection()
    {
        // get the direction from the monster to the player
        return (player.transform.position - transform.position).normalized;
    }

    public void SetMovingPoint(Vector3 point)
    {
        // Debug.Log("set moving point to " + point);
        // set the moving point of the monster
        moving = true;
        movingPoint = point;
    }

    public void TurnToPlayer()
    {
        // turn the monster to face the player
        if (player.transform.position.x < transform.position.x)
        {
            FaceLeft();
        }
        else
        {
            FaceRight();
        }
    }

    public void MoveTo(Vector3 point)
    {
        // move the monster to the point, but ingore the y axis
        SetMovingPoint(new Vector3(point.x, transform.position.y, transform.position.z));
    }

    public void StopMoving()
    {
        moving = false;
    }

    public void FaceLeft()
    {
        // face the monster to the left
        faceLeft = true;
        // if the monster is not facing left, flip the sprite
        if (!lastFaceLeft)
        {
            FlipSprite();
            lastFaceLeft = true;
        }
    }

    public void FaceRight()
    {
        // face the monster to the right
        faceLeft = false;
        // if the monster is not facing right, flip the sprite
        if (lastFaceLeft)
        {
            FlipSprite();
            lastFaceLeft = false;
        }

    }

    public void Damage(int damage)
    {
        // damage the monster
        health -= damage;
        if (health <= 0)
        {
            GetComponent<StateMachine>().ChangeState(dieState.GetType());
        }
        AudioManager.Instance.PlaySound(hitSoundIndex, 0.5f);
    }

    public void FlipSprite()
    {
        // flip the sprite
        // Multiply the sprite's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
