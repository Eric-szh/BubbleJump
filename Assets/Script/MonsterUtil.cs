using Unity.VisualScripting;
using UnityEngine;

public class MonsterUtil : MonoBehaviour
{
    public float speed = 1.0f;
    private Vector3 movingPoint;
    public float movingPointTolerance = 0.1f;
    public bool faceLeft = false;
    public bool lastFaceLeft = true;
    public bool setFaceLeft = false;
    public GameObject player;
    public State restrainState;
    public State dieState;
    private bool moving = true;
    public int health = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (setFaceLeft)
        {
            lastFaceLeft = true;
            FaceLeft();
        }
        else
        {
            lastFaceLeft = false;
            FaceRight();           
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(moving);
        if ((Vector3.Distance(transform.position, movingPoint) > movingPointTolerance) && moving)
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
        GetComponent<StateMachine>().ChangeState(restrainState.GetType());
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
        // set the moving point of the monster
        Debug.Log("moving to"+ point);
        moving = true;
        movingPoint = point;
    }

    public void StopMoving()
    {
        Debug.Log("stop moving");
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
            Debug.Log("face left");
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
            Debug.Log("face right");
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
