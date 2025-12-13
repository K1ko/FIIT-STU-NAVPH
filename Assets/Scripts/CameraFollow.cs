using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothTime = 0.3f;
    public float verticalOffset = 2f;

    private Vector3 velocity = Vector3.zero;

    private float normalSize;
    private float targetSize;
    public float zoomSpeed = 3f;

    void Start()
    {
        Camera cam = GetComponent<Camera>();
        normalSize = cam.orthographicSize;   // this is 10 in your inspector
        targetSize = normalSize;
    }

    void LateUpdate()
    {
        // Keep EXACTLY your original X behavior
        Vector3 targetPosition = new Vector3(
            transform.position.x,                 // DO NOT FOLLOW X
            player.position.y + verticalOffset,   // FOLLOW ONLY Y
            transform.position.z
        );

        transform.position = Vector3.SmoothDamp(
            transform.position, 
            targetPosition, 
            ref velocity, 
            smoothTime
        );

        // Smooth camera zoom
        Camera cam = GetComponent<Camera>();
        cam.orthographicSize = Mathf.Lerp(
            cam.orthographicSize, 
            targetSize, 
            Time.deltaTime * zoomSpeed
        );
    }

    // Snap to player after teleport (fixes delay)
    public void SnapToPlayer(float bossArenaX)
    {
        transform.position = new Vector3(
            bossArenaX,                 // Again: keep X unchanged
            player.position.y + verticalOffset,
            transform.position.z
        );

        velocity = Vector3.zero;
    }

    // Enter boss arena – ONLY change zoom
    public void SetBossZoom(float size)
    {
        targetSize = size;
    }

    // Return to normal gameplay zoom
    public void ResetZoom()
    {
        targetSize = normalSize;
    }
}
