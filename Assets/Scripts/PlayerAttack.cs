using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private float timeBetweenAttacks = 0f;
    public float startTimeBetweenAttacks;

    public Transform attackPos;
    public LayerMask enemyLayers;
    public LayerMask bossLayers;
    public float attackRange;

    public int damage;

    void Update()
    {
        if (timeBetweenAttacks <= 0)
        {
            if (Input.GetKey(KeyCode.F)) // F button
            {
                Debug.Log("Attack!");
                
                // Attack regular enemies
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in enemiesToDamage)
                {
                    enemy.GetComponent<Enemy>().TakeDamage(damage);
                    Debug.Log("Damaged enemy: " + enemy.name);
                }
                // Attack boss
                Collider2D[] bossesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, bossLayers);
                foreach (Collider2D boss in bossesToDamage)
                {
                    boss.GetComponent<Boss>().TakeDamage(damage);
                    Debug.Log("Damaged boss: " + boss.name);
                }
            }
            timeBetweenAttacks = startTimeBetweenAttacks;
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}