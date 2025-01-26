using UnityEngine;

public class TempPlatCtrl : MonoBehaviour
{
    public float lifeTime = 5;
    public float currentLifeTime = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLifeTime += Time.deltaTime;
        if (currentLifeTime >= lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
