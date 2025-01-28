using UnityEngine;
public class TrapCtrl : MonoBehaviour
{
    public float damageCD = 2f;
    float damageTimeCounter = 0;
    bool isActivated = false;
    GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().Damage(10);
            player = collision.gameObject;
            damageTimeCounter = damageCD;
            isActivated = true;
        }
    }

    public void Update()
    {
        if (isActivated)
        {
            damageTimeCounter -= Time.deltaTime;
            if (damageTimeCounter <= 0)
            {
                player.GetComponent<CharacterController2D>().Damage(10);
                damageTimeCounter = damageCD;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isActivated = false;
        }
    }
}
