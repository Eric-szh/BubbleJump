using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogController : MonoBehaviour
{
    [SerializeField] private GameObject dialogPanel; // Reference to the dialog panel
    [SerializeField] private GameObject pulseButton; // Reference to the pulse button


    void Start()
    {
        // Ensure everything is set up correctly at the start
        dialogPanel.SetActive(false);
        pulseButton.SetActive(true); // Pulse button is active initially
    }

    public void OnPulseButtonClicked()
    {
        // Show the dialog panel and stop the game
        dialogPanel.SetActive(true);
        pulseButton.SetActive(false); // Disable the pulse button when the dialog panel is active
        Time.timeScale = 0f; // Pause the game
    }

    public void OnContinueButtonClicked()
    {
        // Resume the game
        dialogPanel.SetActive(false);
        pulseButton.SetActive(true); // Reactivate the pulse button
        Time.timeScale = 1f; // Resume the game
    }

    public void OnBackToMainButtonClicked()
    {
        // Load the starting menu scene
        Time.timeScale = 1f; // Ensure time is running normally
        SceneManager.LoadScene("Starting_Menu");
    }
}