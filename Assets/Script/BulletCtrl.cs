using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float speed = 10.0f;
    public float bulletLife = 2.0f;
    public float dyingime = 0.5f;
    public Sprite deadSprite;
    float timer;
    public bool isLeft = false;
    private bool dead = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > bulletLife)
        {
            Kill();
        }
        
        if (dead)
        {
            return;
        }

        if (isLeft)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
    }

    void Kill()
    {
        dead = true;
        GetComponent<SpriteRenderer>().sprite = deadSprite;
        Invoke("DestroySelf", dyingime);
    }

    void DestroySelf()
    {
        Destroy(gameObject);
    }
}
