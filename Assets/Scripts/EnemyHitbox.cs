using UnityEngine;

//Script for hitbox
public class EnemyHitbox : MonoBehaviour
{
    public float damage = 10f;



    // When the hitbox touches the player, then the player should take damage
    private void OnTriggerEnter(Collider other)
    {
        PlayerStats player =
            other.GetComponent<PlayerStats>();

        if (player == null)
            return;


        player.TakeDamage(damage);
    }
}