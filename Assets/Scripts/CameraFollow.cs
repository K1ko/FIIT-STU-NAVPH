using UnityEngine;

public class CameraFollow : MonoBehaviour   // Smoothly follows the player
{
    public Transform player;
    public float smoothTime = 0.3f;
    public float verticalOffset = 2f;

    private Vector3 velocity = Vector3.zero;

    [Header("Vertical Limits - Normal World")]
    public float normalMinY = 9f;
    public float normalMaxY = 200f;

    [Header("Vertical Limits - Boss Arena")]
    public float bossMinY = 13.2f;
    public float bossMaxY = 35f;

    private float currentMinY;
    private float currentMaxY;

    private float normalSize;
    private float targetSize;
    public float zoomSpeed = 3f;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        normalSize = cam.orthographicSize;
        targetSize = normalSize;

        // Start in normal world
        currentMinY = normalMinY;
        currentMaxY = normalMaxY;
    }

    void LateUpdate()
    {
        if (player == null) return;

        float desiredY = player.position.y + verticalOffset;

        // Clamp Camera Y between currentMinY and currentMaxY
        float clampedY = Mathf.Clamp(desiredY, currentMinY, currentMaxY);

        Vector3 targetPosition = new Vector3(
            transform.position.x,
            clampedY,
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize,
            targetSize,
            Time.deltaTime * zoomSpeed
        );
    }

    public void SnapToPlayer(float bossArenaX)  // Immediately snap camera to player in boss arena
    {
        float desiredY = player.position.y + verticalOffset;
        float clampedY = Mathf.Clamp(desiredY, currentMinY, currentMaxY);

        transform.position = new Vector3(
            bossArenaX,
            clampedY,
            transform.position.z
        );

        velocity = Vector3.zero;
    }

    // Zoom controls
    public void SetBossZoom(float size) => targetSize = size;
    public void ResetZoom() => targetSize = normalSize;

    // Set normal world vertical bounds
    public void SetNormalBounds()
    {
        currentMinY = normalMinY;
        currentMaxY = normalMaxY;
    }

    // Set boss arena vertical bounds
    public void SetBossBounds()
    {
        currentMinY = bossMinY;
        currentMaxY = bossMaxY;
    }
}
