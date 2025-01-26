using UnityEngine;

public class ChargeCtrl : MonoBehaviour
{
    public GameObject frontCharge;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterController2D>().canCharge = true;
            other.gameObject.GetComponent<CharacterController2D>().chargePoint = transform.position;
            other.gameObject.GetComponent<CharacterController2D>().chargeStation = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterController2D>().canCharge = false;
            other.gameObject.GetComponent<CharacterController2D>().chargeStation = null;
        }
    }

    public void startCharge()
    {
        // change the sorting layer of the front charge to 8
        frontCharge.GetComponent<SpriteRenderer>().sortingOrder = 8;
    }

    public void finishCharge()
    {
        // change the sorting layer of the front charge to 0
        frontCharge.GetComponent<SpriteRenderer>().sortingOrder = -1;
    }
}
