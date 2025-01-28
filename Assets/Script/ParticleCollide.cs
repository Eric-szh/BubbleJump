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
            if (objectHit.CompareTag("Stain"))
            {
                Destroy(objectHit.transform.parent.gameObject);
            }

            // Access collision details
            Vector3 collisionPoint = collisionEvent.intersection;
            Vector3 collisionNormal = collisionEvent.normal;
            Vector3 particleVelocity = collisionEvent.velocity;

            if (collisionNormal == Vector3.up)
            {
                // get the size of the particle
                SkillController.Instance.CreateFoam(new Vector3(collisionPoint.x, collisionPoint.y, 0));
            }

        }
    }

    private void OnDestroy()
    {
        SkillController.Instance.RemoveFoam();
    }
}
