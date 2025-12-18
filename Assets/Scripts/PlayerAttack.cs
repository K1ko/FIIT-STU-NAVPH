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
    public InventoryItem swordItem; // Drag your Sword item here in the Inspector


    public Animator anim;


    void Update()
    {
        if (timeBetweenAttacks <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F)) // Attack key
            {
                bool hasSword = InventoryManager.instance.HasItem(swordItem);


                if (anim != null)
                {
                    Debug.Log("Attempting to play attack: " + (hasSword ? "attackSword" : "attackUnarmed"));
                    anim.SetTrigger(hasSword ? "attackSword" : "attackUnarmed");
                }

                Debug.Log("Attack Triggered!");
                timeBetweenAttacks = startTimeBetweenAttacks;
            }
        }
        else
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }

    public void DoAttackHit()
    {
        // Attack regular enemies
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            if (enemy.TryGetComponent(out Enemy e))
            {
                e.TakeDamage(damage);
            }
        }

        // Attack boss
        Collider2D[] bossesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, bossLayers);
        foreach (Collider2D boss in bossesToDamage)
        {
            if (boss.TryGetComponent(out Boss b))
            {
                b.TakeDamage(damage);
            }
        }

        Debug.Log("Attack Hit Applied!");
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}