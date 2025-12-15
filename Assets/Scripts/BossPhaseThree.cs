using UnityEngine;

public class BossPhaseThree : MonoBehaviour
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
    // Switch to Phase 3
    {
        if (newPhase == 3)
        {
            EnterPhaseThree();
        }
    }

    private void EnterPhaseThree()
    // Phase 3 setup
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



        //SpawnCollectibles();
    }


    void Update()
    {
        if (player == null || !this.enabled)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= 25f)
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
