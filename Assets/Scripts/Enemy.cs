using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Enemy took " + damage + " damage. Remaining health: " + health);
        if (health <= 0)
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
