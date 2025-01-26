using UnityEngine;

public class PlatBubbleCrtler : MonoBehaviour
{
    bool floating = false;
    public float floatingSpeed = 0.1f;
    public float floatDesotryTime = 2f;
    public float bubbleLifeTime = 2.5f;
    private float bubbleLifeTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bubbleLifeTimer += Time.deltaTime;
        if (bubbleLifeTimer >= bubbleLifeTime)
        {
            Die();
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Die();
            AudioManager.Instance.PlaySound(3, 0.5f);
        }
        if (col.gameObject.tag == "Paper")
        {
            col.gameObject.GetComponent<PaperCtrl>().TryDestoryPaper();
            floating = true;
            bubbleLifeTimer = -50f;
            GetComponent<Collider2D>().enabled = false;
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            Invoke("Die", floatDesotryTime);
            AudioManager.Instance.PlaySound(3, 0.5f);
        }
        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<MonsterUtil>().Restrain();
            Pop();
            AudioManager.Instance.PlaySound(3, 0.5f);
        }
    }

    private void FixedUpdate()
    {

        if (floating)
        {
            // move the bubble up 
            transform.position = new Vector3(transform.position.x, transform.position.y + floatingSpeed * Time.deltaTime, transform.position.z);
        }

    }

    private void Pop()
    {
        // pop the bubble
        Destroy(gameObject);
    }

    private void Die()
    {
        GetComponent<AniController>().ChangeAnimationState("PlatBubblePop");
    }  
}
