using UnityEngine;

public class ProjectileStaight : MonoBehaviour
{
    public Vector3 direction;
    public float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // move the projectile in the direction
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // destroy the projectile when it hits the player
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<CharacterController2D>().Damage(1);
        } // else using ground layer mask to destroy the projectile when it hits the ground
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
