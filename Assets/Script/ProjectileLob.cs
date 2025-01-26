using UnityEngine;

public class ProjectileLob : MonoBehaviour
{
    public Transform startPoint;  // Start position of the projectile
    public Transform targetPoint; // Target position
    public float projectileSpeed = 10f; // Speed of the projectile
    public Rigidbody2D projectile; // Rigidbody of the projectile to launch
    public float maxHeight = 2f;  // Max height above the start point
    public bool useVector = false; // Use the vector to launch the projectile
    public Vector2 targetPos;

    public void LaunchProjectile()
    {
        Vector2 start = startPoint.position;
        Vector2 target;
        if (useVector)
        {
            target = targetPos;
        } else
        {
            target = targetPoint.position;
        }

        // Gravity
        float gravity = Mathf.Abs(Physics2D.gravity.y);

        // Horizontal distance
        float horizontalDistance = Mathf.Abs(target.x - start.x);

        // Height difference
        float heightDifference = target.y - start.y;

        // Vertical velocity to reach max height
        float verticalVelocity = Mathf.Sqrt(2 * gravity * maxHeight);

        // Time to apex
        float timeToApex = verticalVelocity / gravity;

        // Total flight time (up and down)
        float timeToDescend = Mathf.Sqrt(2 * gravity * (maxHeight - heightDifference)) / gravity;
        float totalFlightTime = timeToApex + timeToDescend;

        // Horizontal velocity
        float horizontalVelocity = horizontalDistance / totalFlightTime;

        // Determine the direction (left or right)
        float direction = (target.x > start.x) ? 1 : -1;

        // Final velocity vector
        Vector2 velocity = new Vector2(direction * horizontalVelocity, verticalVelocity);

        // Apply the velocity to the projectile
        projectile.linearVelocity = velocity;

        Debug.Log($"Launch velocity: {velocity}");
    }

    private void Update()
    {
        // change the projectile direction to face the velocity
        if (projectile.linearVelocity.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // change the transform.left to the velocity direction
        if (projectile.linearVelocity.x < 0)
        {
            transform.right = -projectile.linearVelocity.normalized;
        }
        else
        {
            transform.right = projectile.linearVelocity.normalized;
        }
    }

    void Start()
    {
        // LaunchProjectile();
    }
}
