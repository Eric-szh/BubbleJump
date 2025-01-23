using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene"); // Replace "GameScene" with your actual scene name.
    }

    public void QuitGame()
    {
        Application.Quit(); // Quits the application. Works only in builds, not in the Editor.
    }
}