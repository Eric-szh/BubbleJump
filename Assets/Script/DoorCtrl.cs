using UnityEngine;

public class DoorCtrl : MonoBehaviour
{
    public int doorIndex;
    public bool isLocked = true;
    public bool useButton = false;
    public int idLinked;
    public Sprite openSprite;
    public Sprite closedSprite;
    public bool openAni;
    public string openAniName;
    public bool closeAni;
    public string closeAniName;
    public bool transition;
    public string transitionAni;

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
        if (transition)
        {
            GetComponent<AniController>().ChangeAnimationState(transitionAni);
        }
        else
        {
            UnlockFinish();
        }
    }

    public void UnlockFinish()
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
            if (closeAni)
            {
                GetComponent<AniController>().ChangeAnimationState(closeAniName);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = closedSprite;
            }
            GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            if (openAni)
            {
                GetComponent<AniController>().ChangeAnimationState(openAniName);
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = openSprite;
            }
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
