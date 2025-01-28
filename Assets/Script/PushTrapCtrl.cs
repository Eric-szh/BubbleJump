using UnityEngine;

public class PushTrapCtrl : MonoBehaviour
{
    public bool canPush = false;
    public GameObject player;
    public int pushForce = 20;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPush = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            canPush = false;
        }
    }

    public void Push()
    {
        if (canPush)
        {
            player.GetComponent<CharacterController2D>().BeingPushed(pushForce);
            AudioManager.Instance.PlaySound(18, 0.5f);
        }
    }

    public void StopPush()
    {
         player.GetComponent<CharacterController2D>().StopBeingPushed();
    }
}
