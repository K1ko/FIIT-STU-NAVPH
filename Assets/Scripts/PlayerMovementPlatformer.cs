using UnityEngine;

public class PlayerMovementPlatformer : MonoBehaviour
{
    public float moveSpeed;
    public float jumpVelocity;
    public LayerMask groundLayer;
    private Rigidbody2D RB1;
    private BoxCollider2D boxCollider2d;
    private bool canDoubleJump = false;
    private int jumpCount = 0;
    public float castDistance;
    public Vector2 boxSize;


    void Start()
    {
        RB1 = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        //transform.position += new Vector3(moveHorizontal * moveSpeed * Time.deltaTime, 0, 0);
        RB1.linearVelocity = new Vector2(moveHorizontal * moveSpeed, RB1.linearVelocity.y);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump();
                jumpCount = 1;
            }
            else if (canDoubleJump && jumpCount < 2)
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
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = other.gameObject.transform;
        }
        if (other.gameObject.CompareTag("Trampoline"))
        {
            RB1.linearVelocity = Vector2.up * 20f;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("MovingPlatform"))
        {
            this.transform.parent = null;
        }
    }
    
    void Jump()
    {
        RB1.linearVelocity = Vector2.up * jumpVelocity;
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, castDistance, groundLayer);
        //Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit2d.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }
}
