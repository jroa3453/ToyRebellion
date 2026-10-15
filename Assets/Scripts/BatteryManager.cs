using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BatteryManager : MonoBehaviour
{
    public static BatteryManager Instance;

    [Header("Battery Settings")]
    public float maxBattery = 100f;
    public float currentBattery = 20f;
    public float regenRate = 5f;
    public bool canRegen = false;
    public float autoStartDelay = 5f;

    [Header("UI")]
    public TextMeshProUGUI batteryText;
    public TextMeshProUGUI starsText;
    public Image energyIcon;
    public Image energyBarFill;

    float startTimer;

    void Awake()
    {
        Instance = this;
        canRegen = false;
    }

    public void StartRegen()
    {
        canRegen = true;
    }

    void Update()
    {
        if (!canRegen)
        {
            startTimer += Time.deltaTime;
            if (startTimer >= autoStartDelay) canRegen = true;
        }

        if (canRegen && currentBattery < maxBattery)
        {
            currentBattery += regenRate * Time.deltaTime;
            if (currentBattery > maxBattery) currentBattery = maxBattery;
        }

        if (batteryText != null)
            batteryText.text = Mathf.FloorToInt(currentBattery) + "/" + Mathf.FloorToInt(maxBattery);

        if (starsText != null)
            starsText.text = UpgradeManager.Stars.ToString();

        if (energyBarFill != null)
            energyBarFill.fillAmount = currentBattery / maxBattery;

        if (energyIcon != null)
        {
            bool regenerating = canRegen && currentBattery < maxBattery;
            float pulse = regenerating ? 1f + Mathf.Sin(Time.time * 6f) * 0.08f : 1f;
            energyIcon.transform.localScale = new Vector3(pulse, pulse, 1f);
            energyIcon.color = canRegen ? Color.white : new Color(0.6f, 0.6f, 0.6f, 1f);
        }
    }

    public bool SpendBattery(float amount)
    {
        if (currentBattery < amount) return false;
        currentBattery -= amount;
        return true;
    }
}