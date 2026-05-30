using UnityEngine;
using System;

public class PlayerStats : MonoBehaviour
{
    [Header("Health")]
    public float maxHealth = 100f;

    [Header("Energy")]
    public float maxEnergy = 100f;
    public float energyRegenRate = 20f;

    public float CurrentHealth { get; private set; }
    public float CurrentEnergy { get; private set; }

    public event Action<float, float> HealthChanged;
    public event Action<float, float> EnergyChanged;
    public event Action Died;

    private void Awake()
    {
        CurrentHealth = maxHealth;
        CurrentEnergy = maxEnergy;
    }

    private void Update()
    {
        RegenerateEnergy();
    }

    private void RegenerateEnergy()
    {
        if (CurrentEnergy >= maxEnergy)
            return;

        CurrentEnergy += energyRegenRate * Time.deltaTime;
        CurrentEnergy = Mathf.Min(CurrentEnergy, maxEnergy);

        EnergyChanged?.Invoke(CurrentEnergy, maxEnergy);
    }

    public void TakeDamage(float amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Max(CurrentHealth, 0);

        HealthChanged?.Invoke(CurrentHealth, maxHealth);

        Debug.Log(
            $"Player Health: {CurrentHealth}"
        );

        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
        }
    }

    public bool ConsumeEnergy(float amount)
    {
        if (CurrentEnergy < amount)
            return false;

        CurrentEnergy -= amount;

        EnergyChanged?.Invoke(CurrentEnergy, maxEnergy);

        return true;
    }

}