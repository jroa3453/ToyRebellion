using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [Header("Currency Bar")]
    public TextMeshProUGUI batteriesText;
    public TextMeshProUGUI starsText;

    [Header("Grid Buttons")]
    public Button playButton;
    public Button levelsButton;
    public Button shopButton;
    public Button settingsButton;
    public Button achievementsButton;

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
            starsText.text = PlayerPrefs.GetInt("Stars", 0).ToString();
    }

    void OnPlay()
    {
        string lastLevel = PlayerPrefs.GetString("LastPlayedScene", defaultScene);
        SceneManager.LoadScene(lastLevel);
    }

    void ComingSoon(string buttonName)
    {
        Debug.Log($"{buttonName} — coming soon!");
        // Swap in a popup or toast here later
    }
}