using UnityEngine;

public class BossPhaseTwo : MonoBehaviour
{

    [Header("Attacks")]
    public GameObject firePoint;
    public Transform firePos;


    [Header("References")]
    public Boss boss;

    [Header("Arena Floor")]
    public GameObject normalFloor;
    public GameObject spikeFloor;

    [Header("Collectibles")]
    public GameObject collectiblePrefab;
    public int numberOfCollectibles = 5;
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;

    [Header("Boss Platforms")]
    public Transform bossPlatformsParent;

    private float fireTimer;
    private GameObject player;

    public float fireRate = 1f;

    
    private void Awake()
    {
        if (boss == null)
            boss = GetComponent<Boss>();
    }

    private void OnEnable()
    {
        if (boss != null)
            boss.OnPhaseChanged += HandlePhaseChanged;
    }

    private void OnDisable()
    // Unsubscribe from event
    {
        if (boss != null)
            boss.OnPhaseChanged -= HandlePhaseChanged;
    }

    private void HandlePhaseChanged(int newPhase)
    // Switch to Phase 2
    {
        if (newPhase == 2)
        {
            EnterPhaseTwo();
        }
    }

    private void EnterPhaseTwo()
    // Phase 2 setup
    {
        Debug.Log("BossPhaseTwo: Phase 2 started – destroying minions and switching floors.");

        player = GameObject.FindGameObjectWithTag("Player");

        DestroyAllBossMinions();

        if (normalFloor != null)
            normalFloor.SetActive(false);

        if (spikeFloor != null)
            spikeFloor.SetActive(true);

        if(bossPlatformsParent != null)
            bossPlatformsParent.gameObject.SetActive(true);



        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    // Spawn collectibles on boss platforms
    {
        if (collectiblePrefab == null || bossPlatformsParent == null) return;

        // Get all platform transforms
        Transform[] platforms = bossPlatformsParent.GetComponentsInChildren<Transform>();

        int spawned = 0;
        for (int i = 0; i < numberOfCollectibles; i++)
        {
            // Skip the parent itself
            if (platforms.Length <= 1) break;

            // Pick a random child platform (start at index 1)
            Transform randomPlatform = platforms[Random.Range(1, platforms.Length)];

            // Spawn slightly above platform center
            Vector3 spawnPos = randomPlatform.position + Vector3.up * 1f;

            Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);
            spawned++;
        }

        Debug.Log($"BossPhaseTwo: Spawned {spawned} collectibles on Boss Platforms.");
    }

    void Update()
    {
        if (boss.isDead)
        {
            Debug.Log("Boss is dead, stopping fire.");
            return;
        }
        if (player == null || !this.enabled)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 18f)
        {
            fireTimer += Time.deltaTime;


            if (fireTimer >= fireRate)
            {
                fireTimer = 0f;
                Fire();
            }
        }
    }

    void Fire()
    {
        Instantiate(firePoint, firePos.position, Quaternion.identity);
    }



    private void DestroyAllBossMinions()
    {
        GameObject[] minions = GameObject.FindGameObjectsWithTag("BossMinion");
        Debug.Log("BossPhaseTwo: Found " + minions.Length + " boss minions to destroy.");

        foreach (GameObject m in minions)
        {
            Destroy(m);
        }
    }

}
