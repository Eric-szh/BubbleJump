using UnityEngine;

public class DoorCtrl : MonoBehaviour
{
    public int doorIndex;
    public bool isLocked = true;
    public bool useButton = false;
    public int idLinked;
    public Sprite openSprite;
    public Sprite closedSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        doorIndex = GameStateManager.Instance.RegisterDoor(gameObject);
    }

    public void SetState(bool state)
    {
        isLocked = state;
    }

    public void Unlock()
    {
        isLocked = false;
        GameStateManager.Instance.OpenDoor(doorIndex);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!useButton)
        {
            if (collision.gameObject.tag == "Player")
            {
                if (GameStateManager.Instance.inventory == idLinked)
                {
                    GameStateManager.Instance.UseInventory();
                    Unlock();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked)
        {
            GetComponent<SpriteRenderer>().sprite = closedSprite;
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
