using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Attach to any spawn button. Set batteryRequired to the unit's cost.
/// The button auto-greys when the player can't afford it.
/// Call BatteryButtonGuard.RefreshAll() after any battery change.
/// </summary>
[RequireComponent(typeof(Button))]
public class BatteryButtonGuard : MonoBehaviour
{
    [Tooltip("How many batteries this action costs.")]
    public int batteryRequired = 5;

    [Tooltip("Optional: cost label on the button.")]
    public TextMeshProUGUI costLabel;

    private Button _button;
    private static System.Collections.Generic.List<BatteryButtonGuard> _all
        = new System.Collections.Generic.List<BatteryButtonGuard>();

    void Awake()
    {
        _button = GetComponent<Button>();
        _all.Add(this);
    }

    void OnDestroy()
    {
        _all.Remove(this);
    }

    void Start()
    {
        if (costLabel) costLabel.text = batteryRequired.ToString();
        Refresh();
    }

    public void Refresh()
    {
        int current = GetCurrentBattery();
        bool canAfford = current >= batteryRequired;

        _button.interactable = canAfford;

        // Dim cost label when can't afford
        if (costLabel)
            costLabel.color = canAfford ? Color.white : new Color(1f, 0.3f, 0.3f);
    }

    // Override this if you store battery differently (e.g. in a GameManager singleton)
    int GetCurrentBattery()
    {
        // If you have a GameManager singleton, replace this:
        // return GameManager.Instance.currentBatteries;
        return PlayerPrefs.GetInt("CurrentBatteries", 100);
    }

    /// <summary>Call this whenever battery amount changes.</summary>
    public static void RefreshAll()
    {
        foreach (var guard in _all)
            guard.Refresh();
    }
}