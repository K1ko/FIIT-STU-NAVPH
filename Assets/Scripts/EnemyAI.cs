using UnityEngine;
using UnityEngine.AI;

public class EnemyAI2D : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform[] patrolPoints;
    private int currentPatrolIndex = 0;
    
    [SerializeField] private float stopDistance = 0.2f;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float patrolAreaBuffer = 2f; // Extra distance allowed outside patrol points
    
    private Transform player;
    private Vector2 patrolCenter;
    private float patrolRadius;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        
        // Calculate patrol area bounds
        CalculatePatrolArea();
        
        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.SetDestination(patrolPoints[0].position);
        }

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
                // Check if player is within patrol area before chasing
                if (IsPlayerInPatrolArea())
                {
                    agent.SetDestination(player.position);
                }
                else
                {
                    // Player detected but outside patrol area - return to patrol
                    Patrol();
                }
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

        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    void Patrol()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        if (agent.remainingDistance <= stopDistance)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    void CalculatePatrolArea()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        // Find center of patrol area
        Vector2 sum = Vector2.zero;
        foreach (Transform point in patrolPoints)
        {
            if (point != null)
                sum += (Vector2)point.position;
        }
        patrolCenter = sum / patrolPoints.Length;

        // Find the farthest patrol point to determine radius
        patrolRadius = 0f;
        foreach (Transform point in patrolPoints)
        {
            if (point != null)
            {
                float dist = Vector2.Distance(patrolCenter, point.position);
                if (dist > patrolRadius)
                    patrolRadius = dist;
            }
        }
        
        // Add buffer zone
        patrolRadius += patrolAreaBuffer;
    }

    bool IsPlayerInPatrolArea()
    {
        if (player == null)
            return false;

        float distFromCenter = Vector2.Distance(patrolCenter, player.position);
        return distFromCenter <= patrolRadius;
    }

    void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        
        // Draw patrol points
        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (Transform point in patrolPoints)
            {
                if (point != null)
                    Gizmos.DrawWireSphere(point.position, 0.2f);
            }
        }
        
        // Draw patrol area boundary (only in Play mode or if patrol points exist)
        if (Application.isPlaying)
        {
            Gizmos.color = new Color(0, 1, 0, 0.3f);
            Gizmos.DrawWireSphere(patrolCenter, patrolRadius);
        }
        else if (patrolPoints != null && patrolPoints.Length > 0)
        {
            // Calculate and show patrol area in edit mode too
            Vector2 center = Vector2.zero;
            foreach (Transform point in patrolPoints)
            {
                if (point != null)
                    center += (Vector2)point.position;
            }
            center /= patrolPoints.Length;
            
            float radius = 0f;
            foreach (Transform point in patrolPoints)
            {
                if (point != null)
                {
                    float dist = Vector2.Distance(center, point.position);
                    if (dist > radius)
                        radius = dist;
                }
            }
            radius += patrolAreaBuffer;
            
            Gizmos.color = new Color(0, 1, 0, 0.3f);
            Gizmos.DrawWireSphere(center, radius);
        }
    }
}