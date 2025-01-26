using UnityEngine;

public class ChipCtrl : MonoBehaviour
{
    public GameObject chipPefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<CharacterController2D>().Damage(2); 
            Destroy(gameObject);
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GetComponent<AniController>().ChangeAnimationState("ChipBag_explode");
        }
    }

    public void DestroyChip()
    {
        Destroy(gameObject);
    }

    public void SprayChip() { 
    // generate 4 chips
        for (int i = 0; i < 4; i++)
        {
            // the position should be slightly higher than the gameObject
            Vector3 vector3 = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.5f, gameObject.transform.position.z);
            GameObject chip = Instantiate(chipPefab, vector3, Quaternion.identity);
            chip.GetComponent<ProjectileLob>().startPoint = gameObject.transform;
            // Random target position around the gameObject
            chip.GetComponent<ProjectileLob>().targetPos = new Vector2(gameObject.transform.position.x + Random.Range(-3f, 3f), gameObject.transform.position.y);
            chip.GetComponent<ProjectileLob>().useVector = true;
            chip.GetComponent<ProjectileLob>().LaunchProjectile();
        }
        Destroy(gameObject);
    }
}
