using UnityEngine;

public class BossPhaseTwo : MonoBehaviour
{
    [Header("References")]
    public Boss boss;

    [Header("Arena Floor")]
    public GameObject normalFloor;
    public GameObject spikeFloor;

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
