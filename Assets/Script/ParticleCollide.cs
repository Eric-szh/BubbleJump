using System.Collections.Generic;
using UnityEngine;

public class ParticleCollide : MonoBehaviour
{
    private ParticleSystem particleSystem;

    // List to store collision events
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    void Start()
    {
        // Get the ParticleSystem component
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        // Get collision events
        int numCollisionEvents = particleSystem.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numCollisionEvents; i++)
        {
            ParticleCollisionEvent collisionEvent = collisionEvents[i];

            GameObject objectHit = collisionEvent.colliderComponent.gameObject;
            // Check if the object have the layer stain 
            if (objectHit.layer == LayerMask.NameToLayer("Stain"))
            {
                GameObject parent = objectHit.transform.parent.gameObject;
                Destroy(parent);

            }

            // Access collision details
            Vector3 collisionPoint = collisionEvent.intersection;
            Vector3 collisionNormal = collisionEvent.normal;
            Vector3 particleVelocity = collisionEvent.velocity;

            if (collisionNormal == Vector3.up)
            {
                // find the closet point on the surface of the object
                // for x, round up and - 0.5f
                // for y, round down

                Vector2 vector2 = new Vector2(collisionPoint.x, collisionPoint.y);
                Vector2 roundedVector2 = new Vector2(Mathf.Ceil(vector2.x) - 0.5f, Mathf.Floor(vector2.y));

                SkillController.Instance.CreateFoam(new Vector3(roundedVector2.x, roundedVector2.y, 0));
            }

        }
    }
}
