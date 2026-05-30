using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public PlayerStats playerStats;

    public Slider healthBar;
    public Slider energyBar;

    private void Start()
    {
        playerStats.HealthChanged += UpdateHealth;
        playerStats.EnergyChanged += UpdateEnergy;

        UpdateHealth(
            playerStats.CurrentHealth,
            playerStats.maxHealth);

        UpdateEnergy(
            playerStats.CurrentEnergy,
            playerStats.maxEnergy);
    }

    private void UpdateHealth(float current, float max)
    {
        healthBar.value = current / max;
    }

    private void UpdateEnergy(float current, float max)
    {
        energyBar.value = current / max;
    }

    private void OnDestroy()
    {
        if (playerStats == null)
            return;

        playerStats.HealthChanged -= UpdateHealth;
        playerStats.EnergyChanged -= UpdateEnergy;
    }
}