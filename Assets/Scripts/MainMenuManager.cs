using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Currency Bar")]
    public TextMeshProUGUI batteriesText;
    public TextMeshProUGUI starsText;

    [Header("Player Status")]
    public TextMeshProUGUI levelText;

    [Header("Grid Buttons")]
    public Button playButton;
    public Button levelsButton;
    public Button shopButton;
    public Button settingsButton;
    public Button achievementsButton;
    public Button Quit;
    public GameObject quitConfirmPanel;
    
    [Header("Scene Names")]
    public string defaultScene = "Bedroom"; // fallback if no level saved yet

    void Start()
    {
        RefreshCurrencyBar();

        playButton.onClick.AddListener(OnPlay);
        levelsButton.onClick.AddListener(() => ComingSoon("Levels"));
        shopButton.onClick.AddListener(() => ComingSoon("Shop"));
        settingsButton.onClick.AddListener(() => ComingSoon("Settings"));
        achievementsButton.onClick.AddListener(() => ComingSoon("Achievements"));
       
    }

    void OnEnable()
    {
        RefreshCurrencyBar();
    }

    void RefreshCurrencyBar()
    {
        if (batteriesText != null)
            batteriesText.text = PlayerPrefs.GetInt("Batteries", 0).ToString();

        if (starsText != null)
            starsText.text = UpgradeManager.Stars.ToString();
        
        if (levelText != null)
            levelText.text = "Level " + LevelProgress.HighestUnlocked.ToString();
    }

    public void OnPlay()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void OnUpgradesButtonClicked()
    {
        Debug.Log("Upgrades button clicked, loading Upgrades scene...");
        SceneManager.LoadScene("Upgrades");
    }

    public void ComingSoon(string buttonName)
    {
        Debug.Log($"{buttonName} — coming soon!");
        // Swap in a popup or toast here later
    }

    public void OnQuitButtonClicked()
    {
        quitConfirmPanel.SetActive(true);
    }

    public void OnCancelQuitButtonClicked()
    {
        quitConfirmPanel.SetActive(false);
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}