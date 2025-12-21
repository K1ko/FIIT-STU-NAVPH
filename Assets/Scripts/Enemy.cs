using UnityEngine;

public class Enemy : MonoBehaviour  // Manages enemy health and damage intake
{
    [SerializeField] private float maxHealth = 3f;
    private float currentHealth;

    private HealthBarOther healthBar;
    

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<HealthBarOther>();
        
    }
    public void TakeDamage(int damage)  // Reduces enemy health when taking damage
    {
        currentHealth -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + currentHealth);
        healthBar.UpdateHealthBar(maxHealth, currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
        
    }

    // Removes the enemy from the game when health is 0
    private void Die()
    {
        Debug.Log("Enemy died.");
        Destroy(gameObject);
    }
}
