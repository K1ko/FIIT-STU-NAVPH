using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public float maxHealth = 100;
    public float health;
    public HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(10f);
        }


        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        healthBar.SetHealth(health);
        Debug.Log("Player took " + amount + " damage. Current health: " + health);
    }
}