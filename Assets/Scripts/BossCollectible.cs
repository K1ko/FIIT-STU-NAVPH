using UnityEngine;

public class BossCollectible : MonoBehaviour
{
    public int damageToBoss = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("2D Trigger hit by: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("BossCollectible: Player collected the item.");
            Boss boss = FindObjectOfType<Boss>();

            if (boss != null)
            {
                boss.TakeDamage(damageToBoss);
                Debug.Log("BossCollectible: Damaged boss by " + damageToBoss);
            }

            Destroy(gameObject);
        }
    }


}
