using UnityEngine;

public class FreezeOnTouch : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if the object hit the ground layer
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // freeze the object
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
