using UnityEngine;

public class AreaSound : MonoBehaviour
{
    [SerializeField]
    public int soundIndex;
    public bool stopPlaying = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!stopPlaying)
            {
                AudioManager.Instance.PlayLoopingSound(soundIndex, 0.5f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.StopLoopingSound(soundIndex);
        }
    }

    public void StopSound()
    {
        stopPlaying = true;
        AudioManager.Instance.StopLoopingSound(soundIndex);
    }
}
