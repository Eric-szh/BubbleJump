using UnityEngine;

public class ButtonCtrl : MonoBehaviour
{
    [SerializeField]
    int buttonIndex;
    bool buttonPressed = false;
    public Sprite unpressedSprite;
    public Sprite pressedSprite;
    public DoorCtrl doorToOpen;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        buttonIndex = GameStateManager.Instance.RegisterButton(gameObject);
    }

    public void FixedUpdate()
    {
        if (buttonPressed)
        {
            GetComponent<SpriteRenderer>().sprite = pressedSprite;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = unpressedSprite;
        }
    }
    
    public void SetState(bool state)
    {
        buttonPressed = state;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !buttonPressed)
        {
            GameStateManager.Instance.ButtonPressed(buttonIndex);
            buttonPressed = true;
            doorToOpen.Unlock();
        }
    }


}
