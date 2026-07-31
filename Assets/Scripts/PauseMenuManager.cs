using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
public GameObject pausePanel;
public GameObject settingsPanel;
public Slider volumeSlider;
public GameObject backGround;

   void Start()
    {
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        volumeSlider.value = savedVolume;
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public void TogglePause()
    {
        bool isActive = pausePanel.activeSelf;
        pausePanel.SetActive(!isActive);
        Time.timeScale = isActive ? 1f : 0f; // Pause or resume the game
        backGround.SetActive(!isActive); // Show or hide the background when paused

    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
        backGround.SetActive(false); // Hide the background when resuming
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Resume the game before quitting
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
