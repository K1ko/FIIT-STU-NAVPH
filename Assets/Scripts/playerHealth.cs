using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float health;
    public HealthBar healthBar;

    public float timeSinceLastDamage = 0f;
    public float healingDelay = 5f;      // Time in seconds before healing starts
    public float healingRate = 5f;       // Amount of health to heal per second
    public bool isHealing = false;


    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f);
        }

        // Check if healing should begin
        if (health < maxHealth)
        {
            timeSinceLastDamage += Time.deltaTime;

            if (timeSinceLastDamage >= healingDelay)
            {
                HealOverTime();
            }
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    void HealOverTime()
    // Gradually heal the player over time
    {
        health += healingRate * Time.deltaTime;
        health = Mathf.Min(health, maxHealth); // Clamp to max health
        healthBar.SetHealth(health);
    }



    public void TakeDamage(float amount)
    {
        health -= amount;
        health = Mathf.Max(health, 0); // Clamp to avoid going below 0
        healthBar.SetHealth(health);
        timeSinceLastDamage = 0f;      // Reset healing timer
        Debug.Log("Player took " + amount + " damage. Current health: " + health);
    }

}