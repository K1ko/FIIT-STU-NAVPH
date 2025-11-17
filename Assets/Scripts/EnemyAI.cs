using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform player;
    
    [SerializeField] private float stopDistance = 1f;
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private Vector3[] patrolPoints;
    
    private int currentPatrolIndex = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        
        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0]);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRange)
        {
            agent.SetDestination(player.position);
        }
        else
        {
            Patrol();
        }

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void Patrol()
    {
        if (agent.remainingDistance <= stopDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        if (patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[currentPatrolIndex]);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}