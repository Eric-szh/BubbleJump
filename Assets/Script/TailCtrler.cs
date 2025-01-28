using Unity.VisualScripting;
using UnityEngine;

public class TailCtrler : MonoBehaviour
{
    public bool isAiming = false;
    public bool isShooting = false;
    private bool isLeft = false;
    public SpriteRenderer spriteRenderer;
    public Sprite leftClose;
    public Sprite rightClose;
    public Sprite leftOpen;
    public Sprite rightOpen;
    public Sprite leftAim;
    public Sprite rightAim;
    public Vector3 orignalPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        orignalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = orignalPos;

        Vector3 direction = MouseUtil.getMouseDirection(gameObject.transform.position);
        // Given the direction calcuate if the tail should be left or right
        if (direction.x > 0)
        {
            isLeft = false;
        }
        else
        {
            isLeft = true;
        }

        if (isLeft)
        {
            if (isAiming)
            {
                spriteRenderer.sprite = leftAim;
            }
            else if (isShooting)
            {
                spriteRenderer.sprite = leftOpen;
            }
            else
            {
                spriteRenderer.sprite = leftClose;
            }
        } else
        {
            if (isAiming)
            {
                spriteRenderer.sprite = rightAim;
            }
            else if (isShooting)
            {
                spriteRenderer.sprite = rightOpen;
            }
            else
            {
                spriteRenderer.sprite = rightClose;
            }
        }

        if (isAiming)
        {
            // Rotate the object to face the direction
            gameObject.transform.up = direction;
        } else
        {
            gameObject.transform.up = new Vector3(0, 1, 0);
        }
    }
}
