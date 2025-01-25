using UnityEngine;

public class BodyDetect : MonoBehaviour
{
    public bool insidePlat = false; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwoWayPlat")
        {
            insidePlat = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwoWayPlat")
        {
            insidePlat = false;
        }
    }
}
