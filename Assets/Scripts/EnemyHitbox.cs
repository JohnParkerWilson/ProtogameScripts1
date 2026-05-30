using UnityEngine;

public class EnemyHitbox : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player =
            other.GetComponent<PlayerStats>();

        if (player == null)
            return;

        player.TakeDamage(damage);
    }
}