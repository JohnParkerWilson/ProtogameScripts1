using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    public float speed = 15f;
    public float turnSpeed = 180f;
    public float damage = 25f;
    // So that the projectile doesn't collide with the player or whatever
    public LayerMask damageLayers;

    [Header("Explosion")]
    // Add to make projectile explosive
    public Explosion explosionPrefab;
    public float AOE = 1f;

    // Whatever the player locked on to
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

        // Handle rotation and movement of missile towards target
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

        // TODO: Add explosive functionality
        // If the projectile is explosive
        if (explosionPrefab != null)
        {
            //Create explosive
            Explosion explosion =
                Instantiate(
                    explosionPrefab,
                    transform.position,
                    Quaternion.identity
                );


            explosion.damage = damage;
            explosion.radius = AOE;

            explosion.Explode();
        }
        else
        {
            // Deal Damage
            Health health =
                other.GetComponentInParent<Health>();

            //Debug.Log($"Missile collided with {other.name}");

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
        // Destroy projectile after hitting something
    }
}