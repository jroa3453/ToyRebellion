using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Attach this to a WinScreen or LoseScreen GameObject (or both, one per scene).
/// Call WinLoseManager.ShowResult(true/false) from your GameManager when the
/// match ends — it loads the appropriate scene.
/// </summary>
public class WinLoseManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI resultText;       // "Victory!" / "Defeated!"
    public TextMeshProUGUI rewardText;       // e.g. "+10 Batteries"
    public Button mainMenuButton;
    public Button retryButton;
    public Button selectLevelButton;
    public Button NextLevelButton;


    [Header("Result Config")]
    public bool isWinScreen = true;          // true = Win, false = Lose
    public int batteryReward = 10;           // awarded on win
    public int starReward = 1;

    [Header("Scene Names")]
    public string mainMenuScene = "MainMenu";
    public string retryScene    = "Bedroom";   // scene to reload on retry

    void Start()
    {
        SetupScreen();
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(mainMenuScene));
        retryButton.onClick.AddListener(()    => SceneManager.LoadScene(retryScene));
        selectLevelButton.onClick.AddListener(() => SceneManager.LoadScene("LevelSelect"));
        NextLevelButton.onClick.AddListener(() => SceneManager.LoadScene(LevelProgress.GetSceneName
        (PlayerPrefs.GetInt("LastPlayedLevelIndex", 0))));
    }

    void SetupScreen()
    {
        if (isWinScreen)
        {
            if (resultText)  resultText.text  = "Victory!";
            if (rewardText)  rewardText.text  = $"+{batteryReward} Batteries\n+{starReward} Star";

            // Grant rewards
            int currentBatteries = PlayerPrefs.GetInt("Batteries", 0);
            int currentStars     = PlayerPrefs.GetInt("Stars", 0);

            //LevelProgress
            int currentLevelIndex = PlayerPrefs.GetInt("LastPlayedLevelIndex", 0);
            int currentLevelNumber = currentLevelIndex + 1; // LevelProgress is 1-based
            LevelProgress.UnlockLevel(currentLevelNumber + 1);
            LevelProgress.GetSceneName(currentLevelIndex);

            PlayerPrefs.SetInt("Batteries", currentBatteries + batteryReward);
            PlayerPrefs.SetInt("Stars",     currentStars     + starReward);
            PlayerPrefs.Save();
        }
        else
        {
            if (resultText) resultText.text = "Defeated!";
            if (rewardText) rewardText.text = $"+{batteryReward} Batteries\n+{starReward} Star";
        }
    }

    // ---------------------------------------------------------------
    // Call this from GameManager when a game ends.
    // It sets a PlayerPref flag and loads the correct scene.
    // ---------------------------------------------------------------
    public static void TriggerResult(bool playerWon, string winScene = "WinScreen", string loseScene = "LoseScreen")
    {
        SceneManager.LoadScene(playerWon ? winScene : loseScene);
    }
}