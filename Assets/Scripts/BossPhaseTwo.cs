using UnityEngine;

public class BossPhaseTwo : MonoBehaviour
{
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
    {
        if (boss != null)
            boss.OnPhaseChanged -= HandlePhaseChanged;
    }

    private void HandlePhaseChanged(int newPhase)
    {
        if (newPhase == 2)
        {
            EnterPhaseTwo();
        }
    }

    private void EnterPhaseTwo()
    {
        Debug.Log("BossPhaseTwo: Phase 2 started – destroying minions and switching floors.");

        DestroyAllBossMinions();

        if (normalFloor != null)
            normalFloor.SetActive(false);

        if (spikeFloor != null)
            spikeFloor.SetActive(true);

        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        if (collectiblePrefab == null) return;

        for (int i = 0; i < numberOfCollectibles; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(spawnAreaMin.x, spawnAreaMax.x),
                Random.Range(spawnAreaMin.y, spawnAreaMax.y),
                Random.Range(spawnAreaMin.z, spawnAreaMax.z)
            );

            Instantiate(collectiblePrefab, randomPos, Quaternion.identity);
        }

        Debug.Log("BossPhaseTwo: Spawned " + numberOfCollectibles + " collectibles.");
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
