using UnityEngine;

public class ChargeCtrl : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterController2D>().canCharge = true;
            other.gameObject.GetComponent<CharacterController2D>().chargePoint = transform.position;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<CharacterController2D>().canCharge = false;
        }
    }
}
