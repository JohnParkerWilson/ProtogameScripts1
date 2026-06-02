using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 15f;
    public float turnSpeed = 180f;
    public float damage = 25f;
    // So that the projectile doesn't collide with the player or whatever
    public LayerMask damageLayers;

    private Transform target;

    public void Initialize(
        Transform missileTarget,
        float missileDamage)
    {
        target = missileTarget;
        damage = missileDamage;
    }

    private void Update()
    {
        // If no target, then just fly forward
        if (target == null)
        {
            transform.position +=
                transform.forward *
                speed *
                Time.deltaTime;

            return;
        }

        // Handle rotate missile towards target
        Vector3 direction =
            (target.position - transform.position)
            .normalized;

        Quaternion targetRotation =
            Quaternion.LookRotation(direction);

        transform.rotation =
            Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

        transform.position +=
            transform.forward *
            speed *
            Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Checks if the other object is not an enemy or environment
        if ((damageLayers.value & (1 << other.gameObject.layer)) == 0)
        {
            return;
        }

        // Deal Damage
        Health health =
            other.GetComponent<Health>();

        Debug.Log($"Missile collided with {other.name}");

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
        // Destroy projectile after hitting something
    }
}