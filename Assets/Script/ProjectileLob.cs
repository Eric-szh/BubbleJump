using UnityEngine;

public class ProjectileLob : MonoBehaviour
{
    public Transform startPoint;  // Start position of the projectile
    public Transform targetPoint; // Target position
    public float projectileSpeed = 10f; // Speed of the projectile
    public Rigidbody projectile; // Rigidbody of the projectile to launch

    void LaunchProjectile()
    {
        Vector3 start = startPoint.position;
        Vector3 target = targetPoint.position;
        Vector3 direction = target - start;
        float gravity = Physics.gravity.y;

        // Horizontal distance and height difference
        Vector3 horizontalDirection = new Vector3(direction.x, 0, direction.z);
        float horizontalDistance = horizontalDirection.magnitude;
        float heightDifference = direction.y;

        // Calculate the launch angle
        float angle = Mathf.Asin((gravity * horizontalDistance) / (projectileSpeed * projectileSpeed)) / 2;

        if (float.IsNaN(angle))
        {
            Debug.LogError("No solution found for the given speed and distance.");
            return;
        }

        // Break velocity into components
        float vx = Mathf.Cos(angle) * projectileSpeed;
        float vy = Mathf.Sin(angle) * projectileSpeed;

        // Calculate the velocity vector
        Vector3 velocity = (horizontalDirection.normalized * vx) + (Vector3.up * vy);

        // Apply velocity to the projectile
        projectile.linearVelocity = velocity;
    }
}
