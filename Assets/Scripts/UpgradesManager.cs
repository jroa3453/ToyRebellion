using UnityEngine;
using UnityEngine.UI;

public class UpgradesManager : MonoBehaviour
{
    public void OnPLushieHPButtonClicked()
    {
        Debug.Log("Button Clicked! Current PlushieHPLevel: " + UpgradeManager.PlushieHPLevel);
        UpgradeManager.PurchasePlushieHPUpgrade();
    }
    public void OnPLushieDPSButtonClicked()
    {
        Debug.Log("Button Clicked! Current PlushieDPSLevel: " + UpgradeManager.PlushieDPSLevel);
        UpgradeManager.PurchasePlushieDPSUpgrade();
    }
    public void OnPLushieAttackSPDButtonClicked()
    {
        Debug.Log("Button Clicked! Current PlushieAttackSPDLevel: " + UpgradeManager.PlushieAttackSPDLevel);
        UpgradeManager.PurchasePlushieAttackSPDUpgrade();
    }
}
