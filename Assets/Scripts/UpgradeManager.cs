using System.Runtime.InteropServices;
using UnityEngine;

public static class UpgradeManager
{
    public static int PlushieHPLevel
    {
        get => PlayerPrefs.GetInt("PlushieHPLevel", 0);
        set => PlayerPrefs.SetInt("PlushieHPLevel", value);
    }
     public static int ActionFigHPLevel
    {
        get => PlayerPrefs.GetInt("ActionFigHPLevel", 0);
        set => PlayerPrefs.SetInt("ActionFigHPLevel", value);
    }
     public static int RobotToyHPLevel
    {
        get => PlayerPrefs.GetInt("RobotToyHPLevel", 0);
        set => PlayerPrefs.SetInt("RobotToyHPLevel", value);
    }
    public static int PlushieDPSLevel
    {
        get => PlayerPrefs.GetInt("PlushieDPSLevel", 0);
        set => PlayerPrefs.SetInt("PlushieDPSLevel", value);
    }
    public static int ActionFigDPSLevel
    {
        get => PlayerPrefs.GetInt("ActionFigDPSLevel", 0);
        set => PlayerPrefs.SetInt("ActionFigDPSLevel", value);
    }
    public static int RobotToyDPSLevel
    {
        get => PlayerPrefs.GetInt("RobotToyDPSLevel", 0);
        set => PlayerPrefs.SetInt("RobotToyDPSLevel", value);
    }
    public static int PlayerBaseHPLevel
    {
        get => PlayerPrefs.GetInt("PlayerBaseHPLevel", 0);
        set => PlayerPrefs.SetInt("PlayerBaseHPLevel", value);
    }
    public static int Stars
    {
        get => PlayerPrefs.GetInt("Stars", 0);
        set => PlayerPrefs.SetInt("Stars", value);
    }
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



    public static bool PurchasePlushieHPUpgrade()
    {
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

    public static bool PurchaseActionFigHPUpgrade()
    {
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
    public static bool PurchaseRobotToyHPUpgrade()
    {
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
    public static bool PurchasePlayerBaseHPUpgrade()
    {
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
