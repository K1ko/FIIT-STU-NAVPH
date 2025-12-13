using UnityEngine;

public class BossCollectible : MonoBehaviour
{
    public int damageToBoss = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Boss boss = FindObjectOfType<Boss>();

            if (boss != null)
            {
                boss.TakeDamage(damageToBoss);
                Debug.Log("BossCollectible: Player collected an item and damaged the boss by " + damageToBoss);
            }

            Destroy(gameObject);
        }
    }
}
