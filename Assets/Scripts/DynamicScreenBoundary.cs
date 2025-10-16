using UnityEngine;

public class DynamicScreenBoundary : MonoBehaviour
{
    public Camera mainCamera;
    public BoxCollider2D leftBoundary;
    public BoxCollider2D rightBoundary;

    public float thickness = 0.1f;  // How thick the boundary colliders are

    void Update()
    {
        float camHeight = mainCamera.orthographicSize * 2f;          // Total vertical height
        float camWidth = camHeight * mainCamera.aspect;              // Total horizontal width
        Vector3 camPos = mainCamera.transform.position;

        // Left boundary position and size
        leftBoundary.size = new Vector2(thickness, camHeight);
        leftBoundary.offset = Vector2.zero;
        leftBoundary.transform.position = new Vector3(camPos.x - camWidth / 2f - thickness / 2f, camPos.y, 0);

        // Right boundary position and size
        rightBoundary.size = new Vector2(thickness, camHeight);
        rightBoundary.offset = Vector2.zero;
        rightBoundary.transform.position = new Vector3(camPos.x + camWidth / 2f + thickness / 2f, camPos.y, 0);
    }
}
