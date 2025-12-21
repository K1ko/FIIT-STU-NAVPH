using UnityEngine;

public class BossInitializer : MonoBehaviour    // Activates the boss at the start of the boss fight
{
    public GameObject boss;

    public void ActivateBoss()
    {
        if (boss != null)
        {
            boss.SetActive(true);
            Debug.Log("Boss activated!");
        }
    }
}
