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