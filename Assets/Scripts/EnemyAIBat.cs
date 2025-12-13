using UnityEngine;
using Pathfinding;

public class EnemyAIBat : MonoBehaviour
{
    public enum AIState { Patrol, Chase, Attack }

    public AIState state = AIState.Patrol;

    [Header("Target")]
    public Transform target;

    [Header("Movement")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    [Header("Attack Settings")]
    public float attackCooldown = 1.0f;
    public int attackDamage = 10;
    private float lastAttackTime = 0f;


    

    Path path;

    int currentWaypoint = 0;

    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    [Header("Chase Settings")]
    public float chaseDistance = 10f;        // When bat starts chasing
    public float loseTargetDistance = 15f;   // When bat stops chasing

    [Header("Patrol Settings")]
    public Transform[] patrolPoints;
    private int patrolIndex = 0;
    public float patrolSpeed = 150f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // find components
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case AIState.Patrol:
                Patrol();
                DetectPlayer();
                break;

            case AIState.Chase:
                if (PlayerInChaseRange())
                {
                    FollowPathToTarget();
                }
                else
                {
                    ReturnToPatrol();
                }
                break;
        }
    }

    // --------------------- PATROLLING ---------------------
    private void Patrol()
    {
        if (patrolPoints.Length == 0) return;

        Transform point = patrolPoints[patrolIndex];
        Vector2 dir = ((Vector2)point.position - rb.position).normalized;
        rb.AddForce(dir * patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, point.position) < 0.5f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                lastAttackTime = Time.time;

                // Damage the player
                PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
                if (health != null)
                {
                    health.TakeDamage(attackDamage);
                }

                // Get directions
                Vector2 direction = (collision.transform.position - transform.position).normalized;

                // Knockback Player
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (playerRb != null)
                {
                    Vector2 knockbackDir = (collision.transform.position - transform.position).normalized;
                    float knockbackForce = 800f; 
                    playerRb.AddForce(knockbackDir * knockbackForce);
                }

                // Knockback Bat (in opposite direction)
                float batKnockbackForce = 10f; // less force than player
                rb.AddForce(-direction * batKnockbackForce, ForceMode2D.Impulse);
            }
        }
    }



    private void DetectPlayer()
    {
        if (Vector2.Distance(transform.position, target.position) < chaseDistance)
        {
            state = AIState.Chase;
        }
    }

    private bool PlayerInChaseRange()
    {
        return Vector2.Distance(transform.position, target.position) < loseTargetDistance;
    }

    private void ReturnToPatrol()
    {
        state = AIState.Patrol;
        path = null;
        rb.linearVelocity = Vector2.zero;
    }

    // --------------------- CHASING ---------------------
    private void UpdatePath()
    {
        if (state == AIState.Chase && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p)
    {
        // if path has no error
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    // Update is called once per frame
    private void FollowPathToTarget()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count) return;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void OnDrawGizmos()
    {
        // Chase Radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        // Lose Target Radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, loseTargetDistance);

        // Patrol Path (Green)
        Gizmos.color = Color.green;
        if (patrolPoints != null && patrolPoints.Length > 1)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                Transform p1 = patrolPoints[i];
                Transform p2 = patrolPoints[(i + 1) % patrolPoints.Length];

                if (p1 != null) Gizmos.DrawSphere(p1.position, 0.2f);
                if (p1 != null && p2 != null) Gizmos.DrawLine(p1.position, p2.position);
            }
        }

        if (Application.isPlaying && state == AIState.Chase)
        {
            Gizmos.color = Color.magenta;
            if (target != null)
                Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
