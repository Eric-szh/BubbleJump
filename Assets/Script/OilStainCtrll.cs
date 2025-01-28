using System;
using UnityEngine;

public class OilStainCtrll : MonoBehaviour
{
    public void Start()
    {
        GameStateManager.Instance.DeathEvent += DeathEvent;
    }

    private void DeathEvent()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().ContactOil(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<CharacterController2D>().DeContactOil(gameObject);
        }
    }

    public void OilStay()
    {
        GetComponent<AniController>().ChangeAnimationState("Stain_stay");
    }
}
