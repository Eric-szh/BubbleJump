using UnityEngine;

public class BinBulletCtrl : MonoBehaviour
{
    public GameObject oilStain; // Oil stain object
    public Vector3 offset; // Offset of the oil stain object
    public float destroyDelay = 0f;
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
            Destroy(gameObject,destroyDelay);
            // generate an oil stain object
            

        }
    }


}
