using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovementPlatformer : MonoBehaviour   // Handles player movement, jumping, and platform interactions
{
    public float moveSpeed;
    public float jumpVelocity;
    public LayerMask groundLayer;
    private Rigidbody2D RB1;
    private BoxCollider2D boxCollider2d;
    private bool canDoubleJump = false;
    private int jumpCount = 0;
    public float castDistance;
    public float castXOffset;
    public Vector2 boxSize;

    public Animator anim;
    public AudioSource audioSource;
    public AudioClip jumpClip;
    public AudioClip landClip;
    private bool wasGroundedLastFrame = true;
    public static bool optionsOpen = false;
    void Start()
    {
        RB1 = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        RB1.linearVelocity = new Vector2(moveHorizontal * moveSpeed, RB1.linearVelocity.y);

        // Flip character based on direction
        if (moveHorizontal != 0)
            transform.localScale = new Vector3(Mathf.Sign(moveHorizontal), 1, 1);

        // Set isRunning animation
        anim.SetBool("isRunning", Mathf.Abs(moveHorizontal) > 0.01f);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))   // Open/Close options menu
        {
            if(optionsOpen)
            {
                CloseOptionsMenu();
            }
            else
            {
                OpenOptionsMenu();
            }
        }

        if (Input.GetButtonDown("Jump"))    // Jump key
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
        if (!wasGroundedLastFrame && IsGrounded())  // Play landing sound
        {
            PlayLandSound();
        }

        wasGroundedLastFrame = IsGrounded();
        anim.SetBool("isJumping", !IsGrounded() && Mathf.Abs(RB1.linearVelocity.y) > 0.01f);
    }

    void OnCollisionEnter2D(Collision2D other)  // Handle moving platforms and trampolines
    {
        if (other.gameObject.CompareTag("MovingPlatform") && IsGrounded())
        {
            this.transform.parent = other.gameObject.transform;
        }
        if (other.gameObject.CompareTag("Trampoline") && IsGrounded())
        {
            RB1.linearVelocity = Vector2.up * 30f;
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
        PlayJumpSound();
    }
    public void EnableDoubleJump()
    {
        canDoubleJump = true;
        Debug.Log("Double Jump UNLOCKED");
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2d = Physics2D.BoxCast(transform.position, boxSize, 0f, -transform.up, castDistance, groundLayer);
        //overlapcollider
        //Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, 0.1f, groundLayer);
        return raycastHit2d.collider != null;
    }
    public void PlayJumpSound()
    {
        if (audioSource != null && jumpClip != null)
        {
            audioSource.PlayOneShot(jumpClip);
        }
    }
    public void PlayLandSound()
    {
        if (audioSource != null && landClip != null)
        {
            audioSource.PlayOneShot(landClip);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
    }

    public void OpenOptionsMenu()   // Open options menu
    {
        if (!SceneManager.GetSceneByName("Options").isLoaded)
        {
            OptionsMenu.OpenedFromGame = true;
            Time.timeScale = 0f;
            SceneManager.LoadScene("Options", LoadSceneMode.Additive);
            optionsOpen = true;
        }
    }
    public void CloseOptionsMenu()  // Close options menu
    {
        Time.timeScale = 1f;
        SceneManager.UnloadSceneAsync("Options");
        optionsOpen = false;
    }
}
