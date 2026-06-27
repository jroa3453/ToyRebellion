using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButton : MonoBehaviour
{
    public string sceneName;
    public int levelIndex;

    public void LoadLevel()
    {
        PlayerPrefs.SetString("LastPlayedScene", sceneName);
        PlayerPrefs.SetInt("LastPlayedLevelIndex", levelIndex);
        PlayerPrefs.Save();
        SceneManager.LoadScene(sceneName);
    }
}