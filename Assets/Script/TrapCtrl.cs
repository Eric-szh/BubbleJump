using UnityEngine;

public class TrapCtrl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().Damage(10);
        }
    }
}
