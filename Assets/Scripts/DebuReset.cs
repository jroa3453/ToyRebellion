using UnityEngine;

public class DebugReset : MonoBehaviour
{
    public void ResetLevels()
    {
        LevelProgress.ResetProgress();
        Debug.Log("Progress reset — only Level 1 unlocked now.");
    }
}