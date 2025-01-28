using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    // Time (in seconds) until the object gets destroyed
    public float lifetime = 2f;

    void Start()
    {
        // Destroy this game object after 'lifetime' seconds
        Destroy(gameObject, lifetime);
    }
}

