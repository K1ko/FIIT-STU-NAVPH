using UnityEngine;

public class BossPhaseOne : MonoBehaviour
{
    [Header("Attacks")]
    public GameObject firePoint;
    public Transform firePos;

    [Header("Minions")]
    public GameObject echoMinionPrefab;
    public Transform[] minionSpawnPoints;

    private float fireTimer;
    private float summonTimer;
    private GameObject player;

    public float fireRate = 1f;
    public float summonRate = 5f;

    private Boss boss;

    void Awake()
    {
        boss = GetComponent<Boss>();
    }

    void OnEnable()
    {
        if (boss != null)
            boss.OnPhaseChanged += HandlePhaseChanged;
    }

    void OnDisable()
    {
        if (boss != null)
            boss.OnPhaseChanged -= HandlePhaseChanged;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (player == null || !this.enabled) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 10f)
        {
            fireTimer += Time.deltaTime;
            summonTimer += Time.deltaTime;

            if (fireTimer >= fireRate)
            {
                fireTimer = 0f;
                Fire();
            }

            if (summonTimer >= summonRate)
            {
                summonTimer = 0f;
                SummonMinions();
            }
        }
    }

    void Fire()
    {
        Instantiate(firePoint, firePos.position, Quaternion.identity);
    }

    void SummonMinions()
    {
        if (echoMinionPrefab == null || minionSpawnPoints.Length == 0)
        {
            Debug.LogWarning("No Echo Minion prefab or spawn points assigned.");
            return;
        }

        int minionsToSpawn = Random.Range(1, 4);

        for (int i = 0; i < minionsToSpawn; i++)
        {
            int spawnIndex = Random.Range(0, minionSpawnPoints.Length);

            // Instantiate whole prefab
            GameObject minionRoot = Instantiate(
                echoMinionPrefab,
                minionSpawnPoints[spawnIndex].position,
                Quaternion.identity
            );

            // Find the CHILD that contains the AI script
            EnemyAISpearman minionAI = minionRoot.GetComponentInChildren<EnemyAISpearman>();

            if (minionAI != null)
            {
                // Assign target
                minionAI.target = player.transform;
                minionAI.spawnedByBoss = true;

                // Tag the actual enemy child so Phase Two can destroy it
                minionAI.gameObject.tag = "BossMinion";
            }
            else
            {
                Debug.LogWarning("Minion prefab missing EnemyAISpearman component in children.");
            }
        }

        Debug.Log("Echo Minions summoned.");
    }

    private void HandlePhaseChanged(int newPhase)
    {
        if (newPhase >= 2)
        {
            Debug.Log("Boss entering Phase Two – disabling Phase One.");
            enabled = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 10f);
    }
}
