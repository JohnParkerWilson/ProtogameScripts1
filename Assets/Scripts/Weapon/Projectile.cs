using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public float speed = 80f;
    public float lifetime = 5f;
    public float damage = 10f;

    private Vector3 moveDirection;

    public void Initialize(
        Vector3 direction,
        float projectileDamage)
    {
        moveDirection = direction.normalized;
        damage = projectileDamage;

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.position +=
            moveDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        Health health =
            other.GetComponent<Health>();

        if (health != null)
        {
            health.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}