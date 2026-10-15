using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(Button))]
public class UnitButtonPolish : MonoBehaviour,
    IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    public enum UnitKind { Plushie, ActionFig, RobotToy }

    [Header("Unit")]
    public UnitKind unit;
    public UnitSpawner spawner;       // costs are read from here so labels never go out of sync
    public float fallbackCost = 10f;  // only used if spawner is empty
    public string displayName = "Plushie";

    [Header("Visuals")]
    public Image frame;               // the button's own Image
    public TMP_Text label;            // the button's text
    public Color unitColor = Color.white;

    static readonly Color LockedColor = new Color(0.35f, 0.35f, 0.35f, 1f);

    Button button;
    bool unlocked;
    bool wasAffordable;
    bool pressed;
    float popTimer;
    Vector3 baseScale;

    void Awake()
    {
        button = GetComponent<Button>();
        baseScale = transform.localScale;

        if (spawner == null)
            spawner = FindFirstObjectByType<UnitSpawner>();
    }

    void OnValidate()
    {
        if (frame != null) frame.color = unitColor;
    }

    void OnEnable()
    {
        switch (unit)
        {
            case UnitKind.Plushie:   unlocked = true; break;
            case UnitKind.ActionFig: unlocked = UpgradeManager.ActionFigUnlocked; break;
            case UnitKind.RobotToy:  unlocked = UpgradeManager.RobotToyUnlocked; break;
        }
        button.interactable = unlocked;
    }

    float GetCost()
    {
        if (spawner == null) return fallbackCost;

        switch (unit)
        {
            case UnitKind.Plushie:   return spawner.plushieCost;
            case UnitKind.ActionFig: return spawner.actionFigCost;
            default:                 return spawner.robotToyCost;
        }
    }

    void Update()
    {
        float cost = GetCost();
        bool affordable = unlocked
            && BatteryManager.Instance != null
            && BatteryManager.Instance.currentBattery >= cost;

        // Pop when the unit becomes affordable again
        if (affordable && !wasAffordable)
            popTimer = 0.25f;
        wasAffordable = affordable;

        // Color
        Color target;
        if (!unlocked)        target = LockedColor;
        else if (!affordable) target = unitColor * 0.6f;
        else                  target = unitColor;
        target.a = 1f;

        if (frame != null)
            frame.color = Color.Lerp(frame.color, target, 12f * Time.unscaledDeltaTime);

        // Scale: squish while pressed, quick pop when affordable
        float scaleMult = 1f;
        if (pressed && unlocked) scaleMult = 0.92f;

        if (popTimer > 0f)
        {
            popTimer -= Time.unscaledDeltaTime;
            scaleMult += Mathf.Sin((popTimer / 0.25f) * Mathf.PI) * 0.12f;
        }

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            baseScale * scaleMult,
            18f * Time.unscaledDeltaTime
        );

        // Label
        if (label != null)
        {
            label.text = unlocked
                ? displayName + "\n" + Mathf.RoundToInt(cost)
                : displayName + "\nLOCKED";
        }
    }

    public void OnPointerDown(PointerEventData e) { pressed = true; }
    public void OnPointerUp(PointerEventData e)   { pressed = false; }
    public void OnPointerExit(PointerEventData e) { pressed = false; }
}