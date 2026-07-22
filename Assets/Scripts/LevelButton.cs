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

    public Image cardBackground;

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

        if(unlocked)
        {
            cardBackground.color = LevelProgress.LevelAccentColors[levelIndex];
        }
        else
        {
            cardBackground.color = Color.gray;          
        }

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

    public static readonly Color[] LevelAccentColors =
    {
        new Color(0.18f, 0.37f, 0.43f), // Bedroom - teal
        new Color(0.91f, 0.75f, 0.47f), // Kitchen - cream/gold
        new Color(0.49f, 0.55f, 0.58f), // Garage - grey
        new Color(0.85f, 0.78f, 0.65f), // Hallway - beige
        new Color(0.23f, 0.28f, 0.32f), // Basement - dark blue-grey
        new Color(0.37f, 0.27f, 0.19f), // Attic - brown
        new Color(0.49f, 0.78f, 0.89f), // Backyard - sky blue
        new Color(0.75f, 0.89f, 0.92f), // Bathroom - light blue
        new Color(0.43f, 0.18f, 0.20f), // Den - maroon
        new Color(0.91f, 0.54f, 0.35f), // Rooftop - dusk orange
    };
}