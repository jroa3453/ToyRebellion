using UnityEngine;
using UnityEngine.UI;

public static class UpgradeManager
{

    
    //Plushie Stats
    public static int PlushieHPLevel
    {
        get => PlayerPrefs.GetInt("PlushieHPLevel", 0);
        set => PlayerPrefs.SetInt("PlushieHPLevel", value);
    }
    public static int PlushieDPSLevel
    {
        get => PlayerPrefs.GetInt("PlushieDPSLevel", 0);
        set => PlayerPrefs.SetInt("PlushieDPSLevel", value);
    }
     public static int PlushieAttackSPDLevel
    {
        get => PlayerPrefs.GetInt("PlushieAttackSPDLevel", 0);
        set => PlayerPrefs.SetInt("PlushieAttackSPDLevel", value);
    }
    //ActionFigure Stats
     public static int ActionFigHPLevel
    {
        get => PlayerPrefs.GetInt("ActionFigHPLevel", 0);
        set => PlayerPrefs.SetInt("ActionFigHPLevel", value);
    }
    public static int ActionFigDPSLevel
    {
        get => PlayerPrefs.GetInt("ActionFigDPSLevel", 0);
        set => PlayerPrefs.SetInt("ActionFigDPSLevel", value);
    }
    public static int ActionFigAttackSPDLevel
    {
        get => PlayerPrefs.GetInt("ActionFigAttackSPDLevel", 0);
        set => PlayerPrefs.SetInt("ActionFigAttackSPDLevel", value);
    }
    //Robot Toy Stats
     public static int RobotToyHPLevel
    {
        get => PlayerPrefs.GetInt("RobotToyHPLevel", 0);
        set => PlayerPrefs.SetInt("RobotToyHPLevel", value);
    }
    public static int RobotToyDPSLevel
    {
        get => PlayerPrefs.GetInt("RobotToyDPSLevel", 0);
        set => PlayerPrefs.SetInt("RobotToyDPSLevel", value);
    }
    public static int RobotToyAttackSPDLevel
    {
        get => PlayerPrefs.GetInt("RobotToyAttackSPDLevel", 0);
        set => PlayerPrefs.SetInt("RobotToyAttackSPDLevel", value);
    }
    //Player Base Stats
    public static int PlayerBaseHPLevel
    {
        get => PlayerPrefs.GetInt("PlayerBaseHPLevel", 0);
        set => PlayerPrefs.SetInt("PlayerBaseHPLevel", value);
    }
    //Currency
    public static int Stars
    {
        get => PlayerPrefs.GetInt("Stars", 0);
        set => PlayerPrefs.SetInt("Stars", value);
    }
    public const int MaxUpgradeLevel = 20;
    
    //Upgrade Costs
    public static int GetUpgradeCost(int currentLevel)
    {
        int baseCost = 10;
        int increasePerLevel = 5;

        return baseCost + (currentLevel * increasePerLevel);
    }
    public static float GetUpgradeMultiplier(int level)
    {
        float percentPerLevel = 0.1f;
        return 1 + (level * percentPerLevel);
    }

     public static void ResetStarCurrencyStars()
    {
        Stars = 0;
    }
    //Plushie Upgrades
    public static bool PurchasePlushieHPUpgrade()
    {
        if(PlushieHPLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Plushie HP Upgrade is already at max level.");
                return false;
            }

        int cost = GetUpgradeCost(PlushieHPLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            PlushieHPLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PurchasePlushieDPSUpgrade()
    {
     if(PlushieDPSLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Plushie DPS Upgrade is already at max level.");
                return false;
            }
               
        int cost = GetUpgradeCost(PlushieDPSLevel);
        if(Stars >= cost)
        {
            
            Stars -= cost;
            PlushieDPSLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PurchasePlushieAttackSPDUpgrade()
    {
        if(PlushieAttackSPDLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Plushie Attack SPD Upgrade is already at max level.");
                return false;
            }

        int cost = GetUpgradeCost(PlushieAttackSPDLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            PlushieAttackSPDLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    //ActionFigure Upgrades
    public static bool PurchaseActionFigHPUpgrade()
    {
        if(ActionFigHPLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Action Figure HP Upgrade is already at max level.");
                return false;
            }
        int cost = GetUpgradeCost(ActionFigHPLevel);
        if(Stars >= cost)
        {
           Stars -= cost;
           ActionFigHPLevel += 1;
           return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PurchaseActionFigDPSUpgrade()
    {
        if(ActionFigDPSLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Action Figure DPS Upgrade is already at max level.");
                return false;
            }
        int cost = GetUpgradeCost(ActionFigDPSLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            ActionFigDPSLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PurchaseActionFigAttackSPDUpgrade()
    {
        if(ActionFigAttackSPDLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Action Figure Attack SPD Upgrade is already at max level.");
                return false;
            }
        int cost = GetUpgradeCost(ActionFigAttackSPDLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            ActionFigAttackSPDLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    //Robot Toy Upgrades
    public static bool PurchaseRobotToyHPUpgrade()
    {
        if(RobotToyHPLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Robot Toy HP Upgrade is already at max level.");
                return false;
            }
        int cost = GetUpgradeCost(RobotToyHPLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            RobotToyHPLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool PurchaseRobotToyDPSUpgrade()
    {
        if(RobotToyDPSLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Robot Toy DPS Upgrade is already at max level.");
                return false;
            }
        int cost = GetUpgradeCost(RobotToyDPSLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            RobotToyDPSLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    public static void ResetAllUpgrades()
    {
        
        PlushieHPLevel = 0;
        PlushieDPSLevel = 0;
        PlushieAttackSPDLevel = 0;

        ActionFigHPLevel = 0;
        ActionFigDPSLevel = 0;
        ActionFigAttackSPDLevel = 0;

        RobotToyHPLevel = 0;
        RobotToyDPSLevel = 0;
        RobotToyAttackSPDLevel = 0;
    }
    public static bool PurchaseRobotToyAttackSPDUpgrade()
    {
        if(RobotToyAttackSPDLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Robot Toy Attack SPD Upgrade is already at max level.");
                return false;
            }
        int cost = GetUpgradeCost(RobotToyAttackSPDLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            RobotToyAttackSPDLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
    //Player base Upgrades
    public static bool PurchasePlayerBaseHPUpgrade()
    {
        if(PlayerBaseHPLevel >= MaxUpgradeLevel)
            {
                Debug.Log("Player Base HP Upgrade is already at max level.");
                return false;
            }
        int cost = GetUpgradeCost(PlayerBaseHPLevel);
        if(Stars >= cost)
        {
            Stars -= cost;
            PlayerBaseHPLevel += 1;
            return true;
        }
        else
        {
            return false;
        }
    }
}
