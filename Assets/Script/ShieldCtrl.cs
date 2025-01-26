using UnityEngine;

public class ShieldCtrl : MonoBehaviour
{
    
    public void GainShield()
    {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void LoseShield()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
