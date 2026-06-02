using System.Collections;
using UnityEngine;
using UnityEngine.AI;

//Enemy AI Script
public class EnemyAI : MonoBehaviour
{
    private Transform player; // Used for navigating towards the player

    [Header("Movement")]
    public float chaseRange = 20f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    public float attackAnimLength = 0.4f;

    [Header("Attack Hitbox")]
    [SerializeField]
    private GameObject attackHitbox;

    private float attackTimer;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        //Get's the player's location
        GameObject playerObject =
            GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Update()
    {
        if (player == null)
            return;

        // Moving Enemy to player
        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        // tick down attack timer
        attackTimer -= Time.deltaTime;

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            //Debug.Log("In range to attack");
            agent.ResetPath();

            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (attackTimer > 0)
            return;

        Debug.Log($"{name} is attacking");

        attackTimer = attackCooldown;

        // Attack player with hitbox
        StartCoroutine(AttackRoutine());
    }

    // Attack function
    private IEnumerator AttackRoutine()
    {
        // turn hit box on then off when attacking
        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(attackAnimLength);

        attackHitbox.SetActive(false);
    }
}