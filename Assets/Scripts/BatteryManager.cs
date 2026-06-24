using UnityEngine;
using TMPro;

public class BatteryManager : MonoBehaviour
{
    public static BatteryManager Instance;

    [Header("Battery Settings")]
    public float maxBattery = 100f;
    public float currentBattery = 50f;
    public float regenRate = 8f; // per second

    [Header("UI")]
    public TextMeshProUGUI batteryText;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        // Regenerate over time
        if (currentBattery < maxBattery)
        {
            currentBattery += regenRate * Time.deltaTime;
            currentBattery = Mathf.Clamp(currentBattery, 0, maxBattery);
        }

        // Update UI
        if (batteryText != null)
            batteryText.text = "⚡ " + Mathf.FloorToInt(currentBattery).ToString();
    }

    public bool SpendBattery(float amount)
    {
        if (currentBattery >= amount)
        {
            currentBattery -= amount;
            return true;
        }
        return false; // not enough
    }
}