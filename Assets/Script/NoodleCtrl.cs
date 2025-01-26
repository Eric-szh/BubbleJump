using UnityEngine;

public class NoodleCtrl : MonoBehaviour
{
    public GameObject soupPrefab;
    public Transform soupLocation;
    private bool isFallen = false;
    public float standCd = 10;
    private float standTimer = 20;

    public void Fall()
    {
        GetComponent<AniController>().ChangeAnimationState("Noodle_fall");
    }

    public void Stand()
    {
        GetComponent<AniController>().ChangeAnimationState("Noodle_normal");
    }

    public void Update()
    {
        if (isFallen)
        {
            standTimer += Time.deltaTime;
            if (standTimer >= standCd)
            {
                Stand();
                isFallen = false;
            }
        }
    }

    public void GenSoup() {
        GameObject soup = Instantiate(soupPrefab, soupLocation.position, Quaternion.identity);
    }
    
    public void Fallen()
    {
        GetComponent<AniController>().ChangeAnimationState("Noodle_fallen");
        standTimer = 0;
        isFallen = true;
    }
}
