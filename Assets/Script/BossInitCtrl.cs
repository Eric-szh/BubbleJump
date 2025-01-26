using UnityEngine;

public class BossInitCtrl : MonoBehaviour
{
    public BossWaitState bossWaitState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bossWaitState.Activate();
    }
}
