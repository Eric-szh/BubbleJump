using UnityEngine;

public class FaceCtrl : MonoBehaviour
{
    public Sprite blinkFace;
    public Sprite criticalFace;
    public Sprite woundedFace;
    public Sprite downFace;
    public Sprite upFace;
    public Sprite leftFace;
    public Sprite rightFace;
    public float blinkRate = 0.5f;
    public float blinkChance = 50;
    private bool isBlinking = false;

    // Update is called once per frame
    void Update()
    {
        if (!isBlinking)
        {
            // do a blinking test
            if (Random.Range(0, 100) < blinkChance)
            {
                isBlinking = true;
                GetComponent<SpriteRenderer>().sprite = blinkFace;
                return;
            }
         
        } 

        isBlinking = false;
        int health = transform.parent.gameObject.GetComponent<CharacterController2D>().health;
        if (health == 3)
        {
            Vector3 mouseDir = MouseUtil.getMouseDirection(transform.position);
            // get the angle of the mouse direction
            float angle = Mathf.Atan2(mouseDir.y, mouseDir.x) * Mathf.Rad2Deg;
            // for 45 and 135 degree, we should face up
            if (angle > 45 && angle < 135)
            {
                GetComponent<SpriteRenderer>().sprite = upFace;
            }
            // for 135 and -135 degree, we should face left
            else if (angle > 135 || angle < -135)
            {
                GetComponent<SpriteRenderer>().sprite = leftFace;
            }
            // for -45 and -135 degree, we should face down
            else if (angle < -45 && angle > -135)
            {
                GetComponent<SpriteRenderer>().sprite = downFace;
            }
            // for 45 and -45 degree, we should face right
            else
            {
                GetComponent<SpriteRenderer>().sprite = rightFace;
            }
        } else if (health == 2)
        {
            GetComponent<SpriteRenderer>().sprite = woundedFace;
        } else
        {
            GetComponent<SpriteRenderer>().sprite = criticalFace;
        }

    }
}
