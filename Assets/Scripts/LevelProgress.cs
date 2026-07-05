using UnityEngine;

public static class LevelProgress
{
    private const string KEY = "HighestUnlockedLevel";

    public static int HighestUnlocked
    {
        get => PlayerPrefs.GetInt(KEY, 1);
        private set => PlayerPrefs.SetInt(KEY, value);
    }

    public static void UnlockLevel(int levelNumber)
    {
        if (levelNumber > HighestUnlocked)
        {
            HighestUnlocked = levelNumber;
            PlayerPrefs.Save();
        }
    }

    public static bool IsUnlocked(int levelNumber)
    {
        return levelNumber <= HighestUnlocked;
    }

    public static void ResetProgress()
    {
        PlayerPrefs.SetInt(KEY, 1);
        PlayerPrefs.Save();
    }

    // NEW — add this one
    public static string GetSceneName(int levelIndex)
    {
        string[] levelScenes = { "Bedroom", "Kitchen", "Garage", "Hallway", 
        "Basement", "Attic", "Backyard", "Bathroom", "Den", "Rooftop" };

        int indexToUse = (levelIndex + 1) % 10;

        return levelScenes[indexToUse];
    }
}