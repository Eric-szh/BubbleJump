using UnityEngine;
using UnityEngine.Events; // Add this line


public class UniversalAniEvent : MonoBehaviour
{
    public UnityEvent aniEvent;

    public void AniEvent()
    {
        aniEvent.Invoke();
    }
}
