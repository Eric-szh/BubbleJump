using System.Collections.Generic;
using UnityEngine;

public class SkillController : MonoBehaviour
{
    public static SkillController Instance { get; private set; }
    private List<GameObject> foamList = new List<GameObject>();
    public GameObject oilPrefab;
    public GameObject foamPrefab;

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }

        Instance = this; // Assign the instance
        DontDestroyOnLoad(gameObject); // Prevent destruction on scene load
    }
   

    public void RemoveFoam()
    {
        for (int i = 0; i < foamList.Count; i++)
        {
            Destroy(foamList[i]);
        }
        foamList.Clear();
    }

    public void CreateFoam(Vector3 position)
    {
        // first check if there are any foam objects at the same position
        if (foamList.Count > 0)
        {

            for (int i = 0; i < foamList.Count; i++)
            {
                if (foamList[i].transform.position == position)
                {
                    return;
                }
            }
        }
        GameObject foam = Instantiate(foamPrefab, position, Quaternion.identity);
        foamList.Add(foam);
    }
}
