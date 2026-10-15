using UnityEngine;
using UnityEngine.UI;

public class UnitButtonLock : MonoBehaviour
{
    public enum UnitKind { ActionFig, RobotToy }
    public UnitKind unit;
    public Button button;
    public Image image;

    void Start()
    {
        bool unlocked = unit == UnitKind.ActionFig
            ? UpgradeManager.ActionFigUnlocked
            : UpgradeManager.RobotToyUnlocked;

        button.interactable = unlocked;
        if (image != null)
            image.color = unlocked ? Color.white : new Color(0.4f, 0.4f, 0.4f, 1f);
    }
}