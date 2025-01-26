using UnityEngine;
using UnityEngine.Rendering;

public class PickUpCtrl : MonoBehaviour
{
    int pickupIndex;
    bool pickupGained = false;
    public bool isInventory = false;
    int inventoryIndex;
    public Sprite uncollectedSprite;
    public Sprite collectedSprite;
    public DoorCtrl doorToOpen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pickupIndex = GameStateManager.Instance.RegisterPickup(gameObject);
        if (isInventory)
        {
            inventoryIndex = GameStateManager.Instance.RegisterInventory();
        }
        if (doorToOpen != null)
        {
            doorToOpen.idLinked = pickupIndex;
        }
    }

    public void SetState(bool state)
    {
        pickupGained = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !pickupGained)
        {
            GameStateManager.Instance.PickupGained(pickupIndex);
            pickupGained = true;
            GetComponent<SpriteRenderer>().sprite = collectedSprite;
            if (isInventory)
            {
                GameStateManager.Instance.GainInventory(inventoryIndex, uncollectedSprite);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pickupGained)
        {
            GetComponent<SpriteRenderer>().sprite = collectedSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = uncollectedSprite;
        }
    }
}
