using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public float speed = 80f;
    public float lifetime = 5f;
    public float damage = 10f;

    // So that the projectile doesn't collide with the player or whatever
    public LayerMask damageLayers;

    private Vector3 moveDirection;

    public void Initialize(
        Vector3 direction,
        float projectileDamage)
    {
        moveDirection = direction.normalized;
        damage = projectileDamage;

        Destroy(gameObject, lifetime); // Destroys the projectile at certain amount of time.
        // Could be used to determine range?
    }

    private void Update()
    {
        // Move the projectile in the direction aimed at
        transform.position +=
            moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the other object is not an enemy or environment
        if ((damageLayers.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        // Deal damage
        Health health =
            other.GetComponent<Health>();

        Debug.Log($"Projectile collided with {other.name}");

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
        // Destroy projectile after hitting something
    }
}