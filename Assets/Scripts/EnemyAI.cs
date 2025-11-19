using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2D : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    [SerializeField] private float stopDistance = 0.2f;
    [SerializeField] private float detectionRange = 8f;
    private Transform player;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false; // 2D: prevents movement in Z
        agent.updateRotation = false; // 2D: prevents auto-rotation

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }

        // Find the player once by tag
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        if (playerGO != null)
        {
            player = playerGO.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distToPlayer = Vector2.Distance(transform.position, player.position);
            if (distToPlayer < detectionRange)
            {
                agent.SetDestination(player.position);
            }
            else
            {
                Patrol();
            }
        }
        else
        {
            Patrol();
        }

        // Lock Z axis for pure 2D
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        // If close enough to current patrol point, move to the next
        if (agent.remainingDistance <= stopDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (Transform point in patrolPoints)
            {
                if (point != null)
                    Gizmos.DrawWireSphere(point.position, 0.2f);
            }
        }
    }
}
