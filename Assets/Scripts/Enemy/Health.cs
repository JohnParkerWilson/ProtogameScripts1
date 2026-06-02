using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public System.Action OnDeath;
    private float currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        Debug.Log(name + " took damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(name + " died");
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}