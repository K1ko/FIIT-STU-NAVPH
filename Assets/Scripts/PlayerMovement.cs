using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    private Rigidbody2D rb;

    public float castDistance;
    public LayerMask groundLayer;
    public Vector2 boxSize;

    private bool canDoubleJump = false;
    private int jumpCount = 0;

    private MovingPlatform currentPlatform;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        // Horizontal movement
        float moveX = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveX * moveSpeed, rb.linearVelocity.y);
        
        // Add platform velocity if on a moving platform
        if (currentPlatform != null)
        {
            rb.linearVelocity += currentPlatform.PlatformVelocity;
        }

        // Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded())
            {
                Jump();
                jumpCount = 1;
                currentPlatform = GetCurrentPlatform();
            }
            else if (canDoubleJump && jumpCount < 2) // Double jump
            {
                Jump();
                jumpCount = 2;
            }
        }

        // Toggle double jump on key press 'E' for testing
        if (Input.GetKeyDown(KeyCode.E))
        {
            canDoubleJump = !canDoubleJump;
            Debug.Log("Double Jump Enabled: " + canDoubleJump);
        }
        // Update current platform
        if (isGrounded())
        {
            currentPlatform = GetCurrentPlatform();
        }
        else
        {
            currentPlatform = null;
        }

    }

    void Jump() // Jump function
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public bool isGrounded()    // Ground check function
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
        //if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer ))
        if (hit.collider != null && hit.normal.y > 0.7f)
        {
            return true;
        }
        else {
            return false;
        }
    }
    
    private void OnDrawGizmos() // Visualize the ground check in the editor
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position-transform.up*castDistance, boxSize);
    }

    private MovingPlatform GetCurrentPlatform() // Platform detection
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer);
        if (hit.collider != null)
        {
            return hit.collider.GetComponent<MovingPlatform>();
        }
        return null;
    }
}
