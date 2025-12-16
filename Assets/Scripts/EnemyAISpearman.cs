using UnityEngine;
using UnityEngine.AI;
using Pathfinding;

public class EnemyAISpearman : MonoBehaviour
{
    public enum AIState { Patrol, Chase, Attack }
    public AIState state = AIState.Patrol;

    [Header("Pathfinding")]
    public Transform target;
    public float chaseDistance = 10f;
    public float loseTargetDistance = 15f;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public float patrolSpeed = 150f;
    private int patrolIndex = 0;

    [Header("Attack Settings")]
    public float attackRange = 3f;
    public float attackCooldown = 1.0f;
    public int attackDamage = 10;

private float lastAttackTime = 0f;


    [Header("Physics")]
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int currentWaypoint = 0;

    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;
    [HideInInspector] public bool spawnedByBoss = false;

    private Animator animator;
    private bool isAttacking = false;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Auto-fill patrol points from children if not set
        if (patrolPoints == null || patrolPoints.Length == 0)
    {
        Transform root = transform.root;
        patrolPoints = root.GetComponentsInChildren<Transform>();
        patrolPoints = System.Array.FindAll(patrolPoints, t => t.name.StartsWith("Point"));
    }


        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);

        if (spawnedByBoss)
        {
            state = AIState.Chase;
            chaseDistance = 999f;
            loseTargetDistance = 999f;
        }
    }


    private void FixedUpdate()
    {
        if (target == null)
            return;
        switch (state)
        {
            case AIState.Patrol:
                Patrol();
                DetectPlayer();
                break;

            case AIState.Chase:
                if (PlayerInAttackRange())
                    state = AIState.Attack;
                else if (IsPlayerInChaseRange())
                    PathFollow();
                else
                    ReturnToPatrol();
                break;

            case AIState.Attack:
                AttackPlayer();

                // If player escapes attack range
                if (!PlayerInAttackRange())
                    state = AIState.Chase;

                // If player far away, stop everything
                if (!IsPlayerInChaseRange())
                    ReturnToPatrol();
                break;
        }
    }


    // --------------------- PATROLLING ---------------------
    private void Patrol()
    {
         // Never patrol if spawned by boss
        if (spawnedByBoss)
        {
            state = AIState.Chase;
            return;
        }

        Transform point = patrolPoints[patrolIndex];

        Vector2 dir = ((Vector2)point.position - rb.position).normalized;
        rb.AddForce(dir * patrolSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, point.position) < 1f)
        {
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;
        }

        HandleLookDirection(rb.linearVelocity.x);
    }

    private void DetectPlayer()
    {
        // Start chasing - if the player is within chase distance
        if (Vector2.Distance(transform.position, target.position) < chaseDistance)
        {
            state = AIState.Chase;
        }
    }

    private void ReturnToPatrol()
    {
        if (spawnedByBoss)
        {
            // Never return to patrol, stay in chase mode
            state = AIState.Chase;
            return;
        }
        // Stop chasing and go back to patrol - if the player is beyond lose target distance
        if (Vector2.Distance(transform.position, target.position) > loseTargetDistance)
        {
            state = AIState.Patrol;
            path = null;                    // Remove old chase path
            currentWaypoint = 0;
        }
    }

    private bool IsPlayerInChaseRange()
    {
        // check if player is still within lose target distance
        return Vector2.Distance(transform.position, target.position) < loseTargetDistance;
    }

    // --------------------- CHASING ---------------------
    private void UpdatePath()
    {
        if (state == AIState.Chase && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null) return;
        // reached end of path
        if (currentWaypoint >= path.vectorPath.Count) return;

        Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (jumpEnabled && isGrounded && direction.y > jumpNodeHeightRequirement)
        {
            rb.AddForce(Vector2.up * speed * jumpModifier);
        }

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
        
        // Handle look direction based on velocity
        HandleLookDirection(rb.linearVelocity.x);
    }

    private void HandleLookDirection(float xVelocity)
    {
        if (!directionLookEnabled) return;

        if (xVelocity > 0.05f)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (xVelocity < -0.05f)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    // Check if the target is within activation distance
    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private bool PlayerInAttackRange()
    {
        Debug.Log("Distance to player: " + Vector2.Distance(transform.position, target.position) + " | Attack Range: " + attackRange);
        return Vector2.Distance(transform.position, target.position) < attackRange;
    }

    // private void AttackPlayer()
    // {
    //     // Look at the player
    //     HandleLookDirection((target.position.x - transform.position.x));

    //     // Stop movement when attacking
    //     rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);

    //     // Cooldown check
    //     if (Time.time - lastAttackTime >= attackCooldown)
    //     {
    //         lastAttackTime = Time.time;

    //         // Damage player
    //         PlayerHealth health = target.GetComponent<PlayerHealth>();
    //         if (health != null)
    //         {
    //             health.TakeDamage(attackDamage);
    //         }
    //     }
    // }

    private void AttackPlayer()
    {

        Debug.Log("Spearman in attack range.");
        HandleLookDirection((target.position.x - transform.position.x));

        if (!isAttacking)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); // Stop moving
            animator.SetTrigger("Attack");
            Debug.Log("Spearman attacking!");
            isAttacking = true; // Lock re-attack until animation event unlocks
        }
    }

    public void DealAttackDamage()
    {
        Debug.Log("Spearman dealing damage.");
        if (Vector2.Distance(transform.position, target.position) < attackRange)
        {
            Debug.Log("Spearman hit the player.");
            PlayerHealth health = target.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(attackDamage);
            }
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
        lastAttackTime = Time.time;
    }


    // --------------------- GIZMOS ---------------------
    private void OnDrawGizmos()
    {
        // Chase radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        // Lose target radius
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, loseTargetDistance);

        // Patrol path
        Gizmos.color = Color.green;
        if (patrolPoints != null && patrolPoints.Length > 1)
        {
            for (int i = 0; i < patrolPoints.Length; i++)
            {
                Transform current = patrolPoints[i];
                Transform next = patrolPoints[(i + 1) % patrolPoints.Length];

                if (current != null && next != null)
                {
                    Gizmos.DrawSphere(current.position, 0.2f);
                    Gizmos.DrawLine(current.position, next.position);
                }
            }
        }
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRange);


        // Line to player while chasing
        if (Application.isPlaying && state == AIState.Chase)
        {
            Gizmos.color = Color.magenta;
            if (target != null)
                Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
