using UnityEngine;

public class Stop : MonoBehaviour
{
    // Reference the dialogPanel and the Pulse button directly
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private GameObject pulseButton;

    void Start()
    {
        // Ensure the dialog panel is hidden at the start
        dialogPanel.SetActive(false);
    }

    public void OnPulseButtonClicked()
    {
        // Hide the Pulse button, stop the game, and show the dialog panel
        pulseButton.SetActive(false);
        Time.timeScale = 0f; // Pause the game
        dialogPanel.SetActive(true);
    }
}