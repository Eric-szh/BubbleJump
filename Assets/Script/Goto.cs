using UnityEngine;

public class Goto : MonoBehaviour
{
    public void GotoScene(string sceneName) => UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
}
