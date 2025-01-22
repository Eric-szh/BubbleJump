using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = MouseUtil.getMouseDirection(gameObject.transform.position);

        // Rotate the object to face the direction
        gameObject.transform.up = direction;
    }
}
