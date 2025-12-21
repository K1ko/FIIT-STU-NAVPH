using UnityEngine;

public class PlayerAttack : MonoBehaviour   // Handles player attack mechanics, including animations and damage application
{
    private float timeBetweenAttacks = 0f;
    public float startTimeBetweenAttacks;

    public Transform attackPos;
    public LayerMask enemyLayers;
    public LayerMask bossLayers;
    public float attackRange;

    public int damage;
    public InventoryItem swordItem;


    public Animator anim;


    public AudioSource sfxSource;
    public AudioClip swordSwingSound;
    public AudioClip swordDamageSound;
    public AudioClip unarmedSwingSound;
    public AudioClip unarmedDamageSound;

    void Update()
    {
        if (timeBetweenAttacks <= 0)
        {
            if (Input.GetKeyDown(KeyCode.F)) // Attack key
            {
                bool hasSword = InventoryManager.instance.HasItem(swordItem);   // Check if player has sword
                if (hasSword)   // damage values based on weapon
                {
                    damage = 10;
                }
                else
                {
                    damage = 1;
                }

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

    public void DoAttackHit()   // Called by animation event to apply damage to enemies within range
    {
        // Attack regular enemies
        bool hasWeapon = InventoryManager.instance.HasItem(swordItem);
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in enemiesToDamage)
        {
            if (enemy.TryGetComponent(out Enemy e))
            {
                if (hasWeapon)
                    PlayDamageSound();
                else
                    PlayUnarmedDamageSound();
                e.TakeDamage(damage);
            }
        }

        // Attack boss
        Collider2D[] bossesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, bossLayers);
        foreach (Collider2D boss in bossesToDamage)
        {
            if (boss.TryGetComponent(out Boss b))
            {
                if (hasWeapon)
                    PlayDamageSound();
                else
                    PlayUnarmedDamageSound();
                b.TakeDamage(damage);
            }
        }

        Debug.Log("Attack Hit Applied!");
    }

    // Play attack sound effects
    public void PlaySwordSwingSound()
    {
        sfxSource.PlayOneShot(swordSwingSound);
    }

    public void PlayDamageSound()
    {
        sfxSource.PlayOneShot(swordDamageSound);
    }
    public void PlayUnarmedSwingSound()
    {
        sfxSource.PlayOneShot(unarmedSwingSound);
    }
    public void PlayUnarmedDamageSound()
    {
        sfxSource.PlayOneShot(unarmedDamageSound);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}