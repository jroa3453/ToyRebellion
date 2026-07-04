using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelButton : MonoBehaviour
{
    public string sceneName;
    public int levelIndex;

    [Header("Drag the StatusLabel (TMP) here")]
    public TMP_Text statusLabel;

    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        UpdateLockState();
    }

    void UpdateLockState()
    {
        // levelIndex is 0-based (Bedroom = 0), LevelProgress works 1-based
        int levelNumber = levelIndex + 1;
        bool unlocked = LevelProgress.IsUnlocked(levelNumber);


        Debug.Log($"Checking button : {gameObject.name}, statusLabel is null: {statusLabel == null}");

        button.interactable = unlocked;
        statusLabel.text = unlocked ? "Play" : "Locked";
    }

    public void LoadLevel()
    {
        PlayerPrefs.SetString("LastPlayedScene", sceneName);
        PlayerPrefs.SetInt("LastPlayedLevelIndex", levelIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }
}