using UnityEngine;

public class Damage : MonoBehaviour  // Inflicts damage to the player on collision
{
    public PlayerHealth pHealth;
    public float damage;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}
