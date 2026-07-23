using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
   public GameObject pausePanel;
   public GameObject settingsPanel;

   void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
    
    }

    public void TogglePause()
    {
        bool isActive = pausePanel.activeSelf;
        pausePanel.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f; // Pause or resume the game
    }
}
