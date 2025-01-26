using UnityEngine;

public class SoupCtrl : MonoBehaviour
{
    public GameObject oilPrefab;
    public Transform leftPoint;
    public Transform rightPoint;
    public float rainFeq = 1.0f;
    private float timer = 0.0f;
    public GameObject platPrefab;   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= rainFeq)
        {
            GenOneDrop();
            timer = 0.0f;
        }
    }

    void GenOneDrop()
    {
        // generate a random position in the range of leftPoint and rightPoint
        Vector3 pos = new Vector3(Random.Range(leftPoint.position.x, rightPoint.position.x), leftPoint.position.y, 0);
        // generate a oil drop
        GameObject oil = Instantiate(oilPrefab, pos, Quaternion.identity);
    }

    private void OnDestroy()
    {
        // generate a platform
        GameObject plat = Instantiate(platPrefab, transform.position, Quaternion.identity);
    }
}
