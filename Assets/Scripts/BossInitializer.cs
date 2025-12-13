using UnityEngine;

public class BossInitializer : MonoBehaviour
{
    public GameObject boss;  // the root boss object (disabled initially)

    public void ActivateBoss()
    {
        if (boss != null)
        {
            boss.SetActive(true);
            Debug.Log("Boss activated!");
        }
    }
}
