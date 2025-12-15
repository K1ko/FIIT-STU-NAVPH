using UnityEngine;

public class BossFireScript : MonoBehaviour
{
    public GameObject player;

    private Rigidbody2D rb;

    public float force;

    private float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = (player.transform.position - transform.position);
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotate fire point to face player
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot+210);
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 10.0f)
        {
            Destroy(gameObject);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    // Handle collision with player
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player
            Debug.Log("Player hit by boss fire!");
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(20);
            Debug.Log("Player health: " + collision.gameObject.GetComponent<PlayerHealth>().health);
            Destroy(gameObject);
        }
        // else if (collision.gameObject.CompareTag("Ground"))
        // {
        //     Destroy(gameObject);
        // }
    }
}
