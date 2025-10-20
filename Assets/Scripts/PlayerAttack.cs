using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttacks = 0f;
    public float startTimeBetweenAttacks;

    public Transform attackPos;
    public LayerMask enemies;
    public float attackRange;

    public int damage;

    void Update()
    {
        if (timeBetweenAttacks <= 0)
        {
            if (Input.GetKey(KeyCode.F)) // F button
            {
                Debug.Log("Attack!");
                // Attack code here
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemies);
                foreach (Collider2D enemy in enemiesToDamage)
                {
                    // Here you would typically call a method on the enemy to apply damage
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                    Debug.Log("Damaged " + enemy.name);
                }
            }
            timeBetweenAttacks = startTimeBetweenAttacks;
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }
    
    // Gizmos to visualize attack range
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
