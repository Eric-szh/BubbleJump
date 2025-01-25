using UnityEngine;

public class FeetCtrl : MonoBehaviour
{
    public string whatIsGround;
    void OnTriggerEnter2D(Collider2D other)
    {
        int GroundID = LayerMask.NameToLayer("Ground");
        if (other.gameObject.layer == GroundID)
        {
            Debug.Log("Landed");
            CharacterController2D controller = GetComponentInParent<CharacterController2D>();
            controller.OnLandEvent.Invoke();
            controller.m_Grounded = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        int GroundID = LayerMask.NameToLayer("Ground");
        if (other.gameObject.layer == GroundID)
        {
            CharacterController2D controller = GetComponentInParent<CharacterController2D>();
            controller.m_Grounded = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        int GroundID = LayerMask.NameToLayer("Ground");
        if (other.gameObject.layer == GroundID)
        {
            
            CharacterController2D controller = GetComponentInParent<CharacterController2D>();
            controller.m_Grounded = true;
        }
    }
}
