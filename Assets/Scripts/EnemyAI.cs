using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Transform player;

    [Header("Movement")]
    public float chaseRange = 20f;

    [Header("Combat")]
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;

    [Header("Attack Hitbox")]
    [SerializeField]
    private GameObject attackHitbox;

    private float attackTimer;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

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

        float distance = Vector3.Distance(
            transform.position,
            player.position
        );

        attackTimer -= Time.deltaTime;

        if (distance > attackRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Debug.Log("In range to attack");
            //agent.ResetPath();

            TryAttack();
        }
    }

    private void TryAttack()
    {
        if (attackTimer > 0)
            return;

        Debug.Log($"{name} is attacking");

        attackTimer = attackCooldown;


        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        attackHitbox.SetActive(true);

        yield return new WaitForSeconds(0.2f);

        attackHitbox.SetActive(false);
    }
}