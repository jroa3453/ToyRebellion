using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradesManager : MonoBehaviour
{
    public enum UnitType
    {
        Plushie,
        ActionFig,
        RobotToy
    }

    [Header("Selection")]
    public UnitType selectedUnit = UnitType.Plushie;
    public GameObject PlushiePrefab;
    public GameObject ActionFigPrefab;
    public GameObject RobotToyPrefab;
    GameObject selectedPrefab;

    [Header("Health UI")]
    public TMP_Text hpLevelText;
    public TMP_Text hpValueText;
    public Slider hpSlider;
    public TMP_Text hpCostText;

    [Header("DPS UI")]
    public TMP_Text dpsLevelText;
    public TMP_Text dpsValueText;
    public Slider dpsSlider;
    public TMP_Text dpsCostText;

    [Header("Attack Speed UI")]
    public TMP_Text attackSPDLevelText;
    public TMP_Text attackSPDValueText;
    public Slider attackSPDSlider;
    public TMP_Text attackSPDCostText;

    [Header("Currency")]
    public TMP_Text starsText;

    [Header("Tab Highlights")]
    public GameObject plushieHighLight;
    public GameObject actionFigHighLight;
    public GameObject robotToyHighLight;

    [Header("Unlock UI")]
    public TMP_Text actionFigCostText;
    public TMP_Text robotToyCostText;
    public Image actionFigTabImage;
    public Image robotToyTabImage;

    static readonly Color LockedColor = new Color(0.4f, 0.4f, 0.4f, 1f);

    int hpLevel = 0;
    int dpsLevel = 0;
    int attackSPDLevel = 0;

    void Start()
    {
        RefreshDisplay();
    }

    // ---------------- Display ----------------

    public void RefreshDisplay()
    {
        switch (selectedUnit)
        {
            case UnitType.Plushie:
                hpLevel = UpgradeManager.PlushieHPLevel;
                dpsLevel = UpgradeManager.PlushieDPSLevel;
                attackSPDLevel = UpgradeManager.PlushieAttackSPDLevel;
                selectedPrefab = PlushiePrefab;
                break;
            case UnitType.ActionFig:
                hpLevel = UpgradeManager.ActionFigHPLevel;
                dpsLevel = UpgradeManager.ActionFigDPSLevel;
                attackSPDLevel = UpgradeManager.ActionFigAttackSPDLevel;
                selectedPrefab = ActionFigPrefab;
                break;
            case UnitType.RobotToy:
                hpLevel = UpgradeManager.RobotToyHPLevel;
                dpsLevel = UpgradeManager.RobotToyDPSLevel;
                attackSPDLevel = UpgradeManager.RobotToyAttackSPDLevel;
                selectedPrefab = RobotToyPrefab;
                break;
        }

        Unit unitData = selectedPrefab.GetComponent<Unit>();

        hpLevelText.text = "Lv " + hpLevel;
        hpSlider.value = (float)hpLevel / UpgradeManager.MaxUpgradeLevel;
        float actualHP = unitData.health * UpgradeManager.GetUpgradeMultiplier(hpLevel);
        hpValueText.text = "<color=#FFD700>" + actualHP + "</color> HP";
        hpCostText.text = UpgradeManager.GetUpgradeCost(hpLevel).ToString();

        dpsLevelText.text = "Lv " + dpsLevel;
        dpsSlider.value = (float)dpsLevel / UpgradeManager.MaxUpgradeLevel;
        float actualDPS = unitData.attackDamage * UpgradeManager.GetUpgradeMultiplier(dpsLevel);
        dpsValueText.text = "<color=#FFD700>" + actualDPS + "</color> DPS";
        dpsCostText.text = UpgradeManager.GetUpgradeCost(dpsLevel).ToString();

        attackSPDLevelText.text = "Lv " + attackSPDLevel;
        attackSPDSlider.value = (float)attackSPDLevel / UpgradeManager.MaxUpgradeLevel;
        float actualAttackSPD = unitData.attackRate * UpgradeManager.GetUpgradeMultiplier(attackSPDLevel);
        attackSPDValueText.text = "<color=#FFD700>" + actualAttackSPD + "</color> ATTSPD";
        attackSPDCostText.text = UpgradeManager.GetUpgradeCost(attackSPDLevel).ToString();

        starsText.text = UpgradeManager.Stars.ToString();

        plushieHighLight.SetActive(selectedUnit == UnitType.Plushie);
        actionFigHighLight.SetActive(selectedUnit == UnitType.ActionFig);
        robotToyHighLight.SetActive(selectedUnit == UnitType.RobotToy);

        // Tab lock state
        bool actionFigUnlocked = UpgradeManager.ActionFigUnlocked;
        actionFigTabImage.color = actionFigUnlocked ? Color.white : LockedColor;
        actionFigCostText.gameObject.SetActive(!actionFigUnlocked);

        bool robotToyUnlocked = UpgradeManager.RobotToyUnlocked;
        robotToyTabImage.color = robotToyUnlocked ? Color.white : LockedColor;
        robotToyCostText.gameObject.SetActive(!robotToyUnlocked);
    }

    // ---------------- Unit tabs (tapping a locked tab buys it) ----------------

    public void SelectPlushie()
    {
        selectedUnit = UnitType.Plushie;
        RefreshDisplay();
    }

    public void SelectActionFig()
    {
        if (!UpgradeManager.PurchaseActionFigUnlock())
        {
            RefreshDisplay();
            return; // not enough Stars
        }
        selectedUnit = UnitType.ActionFig;
        RefreshDisplay();
    }

    public void SelectRobotToy()
    {
        if (!UpgradeManager.PurchaseRobotToyUnlock())
        {
            RefreshDisplay();
            return; // not enough Stars
        }
        selectedUnit = UnitType.RobotToy;
        RefreshDisplay();
    }

    // ---------------- Upgrade buttons (act on the selected unit) ----------------

    public void OnHPButtonClicked()
    {
        switch (selectedUnit)
        {
            case UnitType.Plushie:   UpgradeManager.PurchasePlushieHPUpgrade(); break;
            case UnitType.ActionFig: UpgradeManager.PurchaseActionFigHPUpgrade(); break;
            case UnitType.RobotToy:  UpgradeManager.PurchaseRobotToyHPUpgrade(); break;
        }
        RefreshDisplay();
    }

    public void OnDPSButtonClicked()
    {
        switch (selectedUnit)
        {
            case UnitType.Plushie:   UpgradeManager.PurchasePlushieDPSUpgrade(); break;
            case UnitType.ActionFig: UpgradeManager.PurchaseActionFigDPSUpgrade(); break;
            case UnitType.RobotToy:  UpgradeManager.PurchaseRobotToyDPSUpgrade(); break;
        }
        RefreshDisplay();
    }

    public void OnAttackSPDButtonClicked()
    {
        switch (selectedUnit)
        {
            case UnitType.Plushie:   UpgradeManager.PurchasePlushieAttackSPDUpgrade(); break;
            case UnitType.ActionFig: UpgradeManager.PurchaseActionFigAttackSPDUpgrade(); break;
            case UnitType.RobotToy:  UpgradeManager.PurchaseRobotToyAttackSPDUpgrade(); break;
        }
        RefreshDisplay();
    }

    // ---------------- Debug / test buttons ----------------

    public void AddTestStars()
    {
        UpgradeManager.Stars += 250;
        RefreshDisplay();
    }

    public void OnResetButtonClicked()
    {
        UpgradeManager.ResetAllUpgrades();
        selectedUnit = UnitType.Plushie; // ActionFig/RobotToy are locked again after reset
        RefreshDisplay();
    }

    public void OnResetCurrencyStarsButtonClicked()
    {
        UpgradeManager.ResetStarCurrencyStars();
        RefreshDisplay();
    }
}