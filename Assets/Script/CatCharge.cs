using UnityEngine;

public class CatCharge : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void finishCharge()
    {
        transform.parent.GetComponent<CharacterController2D>().ChargeRelease();
    }

    public void fullHealth()
    {
        transform.parent.GetComponent<CharacterController2D>().health = 3;
    }
}
