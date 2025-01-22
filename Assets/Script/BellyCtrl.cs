using UnityEngine;

public class BellyCtrl : MonoBehaviour
{
    private bool isLeft = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = transform.parent.gameObject;
        Vector3 velocity = player.GetComponent<Rigidbody2D>().linearVelocity;
        int health = player.GetComponent<CharacterController2D>().health;
        if (velocity.x < 0)
        {
            isLeft = true;
        }
        else if (velocity.x > 0)
        {
            isLeft = false;
        } 
        if (isLeft)
        {
            switch (health)
            {
                case 3:
                    GetComponent<AniController>().ChangeAnimationState("Belly3");
                    break;
                case 2:
                    GetComponent<AniController>().ChangeAnimationState("Belly2L");
                    break;
                case 1:
                    GetComponent<AniController>().ChangeAnimationState("Belly1L");
                    break;
            }
        } else
        {
            switch (health)
            {
                case 3:
                    GetComponent<AniController>().ChangeAnimationState("Belly3");
                    break;
                case 2:
                    GetComponent<AniController>().ChangeAnimationState("Belly2R");
                    break;
                case 1:
                    GetComponent<AniController>().ChangeAnimationState("Belly1R");
                    break;
            }
        }
      
    }
}
