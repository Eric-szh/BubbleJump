using UnityEngine;

public class OilBulletCtrl : MonoBehaviour
{
    public GameObject oilStain; // Oil stain object
    public Vector3 offset; // Offset of the oil stain object
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // OnTriggerEnter2D is called when another collider enters the trigger
    void OnTriggerEnter2D(Collider2D other)
    {
        // using layer mask to check if the object is a ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {

            // Destroy the bullet
            Destroy(gameObject);
            // generate an oil stain object
            Instantiate(oilStain, transform.position + offset, Quaternion.identity);

        }
    }


}
