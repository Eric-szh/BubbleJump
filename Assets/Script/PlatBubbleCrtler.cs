using UnityEngine;

public class PlatBubbleCrtler : MonoBehaviour
{
    bool activated = false;
    bool floating = false;
    public float secondstoDestroy = 2f;
    public float floatingSpeed = 0.1f;
    public float floatDesotryTime = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            activated = true;
        }
        if (col.gameObject.tag == "Paper")
        {
            Destroy(col.gameObject);
            floating = true;
            Invoke("Pop", floatDesotryTime);
        }
    }

    private void FixedUpdate()
    {
        if (activated)
        {
            // descrease the opacity of the bubble
            Color color = GetComponent<SpriteRenderer>().color;
            color.a -= 1 / secondstoDestroy * Time.deltaTime;
            GetComponent<SpriteRenderer>().color = color;
        }

        if (floating)
        {
            // move the bubble up 
            transform.position = new Vector3(transform.position.x, transform.position.y + floatingSpeed * Time.deltaTime, transform.position.z);
        }

        if (GetComponent<SpriteRenderer>().color.a <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Pop()
    {
        // pop the bubble
        Destroy(gameObject);
    }
}
