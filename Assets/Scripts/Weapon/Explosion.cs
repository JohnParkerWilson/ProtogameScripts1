using UnityEngine;

public class Explosion : MonoBehaviour
{
    // To be used to add AOE/Explosion functionality to weapons.
    [Header("Explosion")]
    public float radius = 5f;
    public float damage = 25f;

    [Header("Debug")]
    public bool showDebugSphere = true;

    public void Explode()
    {
        Collider[] hits =
            Physics.OverlapSphere(
                transform.position,
                radius
            );

        // For each enemy the sphere collides with, deal damage to them
        foreach (Collider hit in hits)
        {
            // Deal damage
            Health health =
                hit.GetComponentInParent<Health>();

            if (health != null)
            {
                health.TakeDamage(damage);
            }
        }

        CreateVisual();

        Destroy(gameObject);
    }

    // Create Visual for explosion
    private void CreateVisual()
    {
        if (!showDebugSphere)
            return;

        GameObject sphere =
            GameObject.CreatePrimitive(
                PrimitiveType.Sphere);

        sphere.transform.position =
            transform.position;

        sphere.transform.localScale =
            Vector3.one * radius * 2f;

        Destroy(
            sphere.GetComponent<Collider>()
        );

        Destroy(sphere, 0.25f);
    }
}