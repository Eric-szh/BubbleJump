using UnityEngine;

public class AreaSound : MonoBehaviour
{
    [SerializeField]
    public int soundIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlayLoopingSound(soundIndex, 0.5f);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.StopLoopingSound(soundIndex);
        }
    }
}
