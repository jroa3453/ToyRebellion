using UnityEngine;
using UnityEngine.UI;

public class BaseHealth : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 1000f;
    public float currentHealth;
    public bool isPlayerBase;

    [Header("UI")]
    public Slider healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    public void TakeDamage(float amount)
    {
        Debug.Log(gameObject.name + " TakeDamage called: " + amount + " current health: " + currentHealth);
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (healthBar != null)
            healthBar.value = currentHealth;

        if (currentHealth <= 0)
            GameOver();
    }

    void GameOver()
    {
        if (isPlayerBase)
        {
            Debug.Log("GAME OVER - Player lost!");
            WinLoseManager.TriggerResult(false);
        }
        else
        {
            Debug.Log("VICTORY - Player won!");
            WinLoseManager.TriggerResult(true);
        }
        // We'll add proper game over screen later
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}