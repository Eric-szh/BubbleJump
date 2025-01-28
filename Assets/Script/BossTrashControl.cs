using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class BossTrashControl : MonoBehaviour
{
    public List<Sprite> trashList;
    public GameObject trashPrefab;
    public Transform trashLocation;
    private bool isFallen = false;
    public float standCd = 10;
    private float standTimer = 20;

    public float leftBound;
    public float rightBound;
    public int y;
    public int numTrash = 5;

    public void Fall()
    {
        GetComponent<AniController>().ChangeAnimationState("Trash_fall");
    }

    public void Stand()
    {
        GetComponent<AniController>().ChangeAnimationState("Trash_normal");
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

    public void GenTrash()
    {
        for (int i = 0; i < numTrash; i++)
        {
            GenSingleTrash();
        }
    }

    public void GenSingleTrash()
    {
        GameObject trash = Instantiate(trashPrefab, trashLocation.position, Quaternion.identity);
        trash.GetComponent<SpriteRenderer>().sprite = trashList[Random.Range(0, trashList.Count)];

        trash.GetComponent<ProjectileLob>().startPoint = trashLocation;
        // Randomize the target position of the trash, using float values for the x and y coordinates
        trash.GetComponent<ProjectileLob>().targetPos = new Vector2(Random.Range(leftBound, rightBound), y);
        trash.GetComponent<ProjectileLob>().useVector = true;
        trash.GetComponent<ProjectileLob>().maxHeight = 0.7f;
        trash.GetComponent<ProjectileLob>().LaunchProjectile();
    }

    public void Fallen()
    {
        GetComponent<AniController>().ChangeAnimationState("Trash_fallen");
        standTimer = 0;
        isFallen = true;
    }
}
