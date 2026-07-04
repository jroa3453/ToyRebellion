using UnityEngine;

public class UnitHealthBar : MonoBehaviour
{
    [Header("UI")]
    public float currentHealth = 15f;
    public float maxHealth = 15f;
    public UnityEngine.UI.Slider healthBar;

    void Start()
    {
        if (healthBar != null)
        healthBar.gameObject.SetActive(false);
    }

    public void SetMaxHealth(float max)
    {
        maxHealth = max;
        currentHealth = max;
    }

    public void SetHealth(float health)
    {
        currentHealth = health;

        if (healthBar != null)
        {
            healthBar.gameObject.SetActive(true);
            healthBar.value = currentHealth;
        }
    }

}
