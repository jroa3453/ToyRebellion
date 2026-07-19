using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesManager : MonoBehaviour
{
    public UnitType selectedUnit = UnitType.Plushie;
    public GameObject PlushiePrefab;
    public GameObject ActionFigPrefab;
    public GameObject RobotToyPrefab;
    GameObject selectedPrefab;

    public TMP_Text hpLevelText;
    public TMP_Text hpValueText;
    public Slider hpSlider;

    public TMP_Text dpsLevelText;
    public TMP_Text dpsValueText;
    public Slider dpsSlider;

    public TMP_Text attackSPDLevelText;
    public TMP_Text attackSPDValueText;
    public Slider attackSPDSlider;

    int hpLevel = 0;
    int dpsLevel = 0;
    int attackSPDLevel = 0;

    public void OnPLushieHPButtonClicked()
{
    Debug.Log("Before: PlushieHPLevel=" + UpgradeManager.PlushieHPLevel + " Stars=" + UpgradeManager.Stars);
    bool success = UpgradeManager.PurchasePlushieHPUpgrade();
    Debug.Log("Purchase success? " + success + " | After: PlushieHPLevel=" + UpgradeManager.PlushieHPLevel + " Stars=" + UpgradeManager.Stars);
    RefreshDisplay();
}
    public void OnPLushieDPSButtonClicked()
    {
        Debug.Log("Button Clicked! Current PlushieDPSLevel: " + UpgradeManager.PlushieDPSLevel);
        UpgradeManager.PurchasePlushieDPSUpgrade();
        RefreshDisplay();
    }
    public void OnPLushieAttackSPDButtonClicked()
    {
        Debug.Log("Button Clicked! Current PlushieAttackSPDLevel: " + UpgradeManager.PlushieAttackSPDLevel);
        UpgradeManager.PurchasePlushieAttackSPDUpgrade();
        RefreshDisplay();
    }
    public enum UnitType
    {
        Plushie,
        ActionFig,
        RobotToy
    }

    void Start()
    {
        RefreshDisplay();
    }
    public void AddTestStars()
{
    UpgradeManager.Stars += 250;
    Debug.Log("Stars added! Current Stars: " + UpgradeManager.Stars);
    RefreshDisplay();
}

    public void RefreshDisplay()
    {
        // Update the display for the selected unit type
        switch (selectedUnit)
        {
            case UnitType.Plushie:
                Debug.Log("Refreshing display for Plushie");
                hpLevel = UpgradeManager.PlushieHPLevel;
                dpsLevel = UpgradeManager.PlushieDPSLevel;
                attackSPDLevel = UpgradeManager.PlushieAttackSPDLevel;
                selectedPrefab = PlushiePrefab;
                break;
            case UnitType.ActionFig:
                Debug.Log("Refreshing display for Action Figure");
                 hpLevel = UpgradeManager.ActionFigHPLevel;
                dpsLevel = UpgradeManager.ActionFigDPSLevel;
                attackSPDLevel = UpgradeManager.ActionFigAttackSPDLevel;
                selectedPrefab = ActionFigPrefab;
                break;
            case UnitType.RobotToy:
                Debug.Log("Refreshing display for Robot Toy");
                hpLevel = UpgradeManager.RobotToyHPLevel;
                dpsLevel = UpgradeManager.RobotToyDPSLevel;
                attackSPDLevel = UpgradeManager.RobotToyAttackSPDLevel;
                selectedPrefab = RobotToyPrefab;
                break;
        }
            hpLevelText.text = "Lv " + hpLevel;
            hpSlider.value = (float)hpLevel / UpgradeManager.MaxUpgradeLevel;
            Unit unitData = selectedPrefab.GetComponent<Unit>();
            float actualHP = unitData.health * UpgradeManager.GetUpgradeMultiplier(hpLevel);
            hpValueText.text = "<color=#FFD700>" + actualHP + "</color> HP";

            dpsLevelText.text = "Lv " + dpsLevel;
            dpsSlider.value = (float)dpsLevel / UpgradeManager.MaxUpgradeLevel;
            float actualDPS = unitData.attackDamage * UpgradeManager.GetUpgradeMultiplier(dpsLevel);
            dpsValueText.text = "<color=#FFD700>" + actualDPS + "</color> DPS";

            attackSPDLevelText.text = "Lv " + attackSPDLevel;
            attackSPDSlider.value = (float)attackSPDLevel / UpgradeManager.MaxUpgradeLevel;
            float actualAttackSPD = unitData.attackRate * UpgradeManager.GetUpgradeMultiplier(attackSPDLevel);
            attackSPDValueText.text = "<color=#FFD700>" + actualAttackSPD + "</color> SPD";
    }

    public void SelectPlushie()
    {
        selectedUnit = UnitType.Plushie;
        RefreshDisplay();
    }
    public void SelectActionFig()
    {
        selectedUnit = UnitType.ActionFig;
        RefreshDisplay();
    }
    public void SelectRobotToy()
    {
        selectedUnit = UnitType.RobotToy;
        RefreshDisplay();
    }
}
