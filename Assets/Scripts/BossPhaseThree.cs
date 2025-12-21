using UnityEngine;

public class BossPhaseThree : MonoBehaviour // Manages Boss Phase Three behavior
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
    public Vector3 spawnAreaMin;
    public Vector3 spawnAreaMax;

    [Header("Boss Platforms")]
    public Transform bossPlatformsParent;

    private float fireTimer;
    private GameObject player;

    public float fireRate = 0.5f;

    
    private void Awake()
    {
        if (boss == null)
            boss = GetComponent<Boss>();
    }

    private void OnEnable() // Subscribe to event
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
    // Switch to Phase 3
    {
        if (newPhase == 3)
        {
            EnterPhaseThree();
        }
    }

    private void EnterPhaseThree()
    // Phase 3 setup, switch floors, cleanup collectibles and disable platforms
    {
        Debug.Log("BossPhaseThree: Phase 3 started – switching floors.");

        player = GameObject.FindGameObjectWithTag("Player");

        DestroyAllBossCollectibles();


        if (normalFloor != null)
            normalFloor.SetActive(true);

        if (spikeFloor != null)
            spikeFloor.SetActive(false);

        if(bossPlatformsParent != null)
            bossPlatformsParent.gameObject.SetActive(false);
    }


    void Update()   // Handle firing at player
    {
        if (boss.isDead)
        {
            Debug.Log("Boss is dead, stopping fire.");
            return;
        }
        if (player == null || boss == null || boss.isDead)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 25f)
        {
            fireTimer += Time.deltaTime;


            if (fireTimer >= fireRate && !boss.isDead)
            {
                fireTimer = 0f;
                Fire();
            }
        }
    }

    void Fire()
    {
        Instantiate(firePoint, firePos.position, Quaternion.identity);
        boss.PlayHitSound();
    }

    private void DestroyAllBossCollectibles()
    // Destroy all collectibles spawned by the boss
    {
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("BossCollectible");

        Debug.Log("BossPhaseThree: Destroying " + collectibles.Length + " collectibles.");

        foreach (GameObject c in collectibles)
        {
            Destroy(c);
        }
    }

}
