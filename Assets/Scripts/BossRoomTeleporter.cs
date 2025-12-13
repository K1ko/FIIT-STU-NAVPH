using UnityEngine;

public class BossRoomTeleporter : MonoBehaviour
{
    public Transform TeleportPoint;     // where the player appears in the boss room
    public GameObject boss;          // add this
    public Camera mainCamera;

    private bool triggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (triggered) return;

        if (collision.CompareTag("Player"))
        {
            triggered = true;

            collision.transform.position = TeleportPoint.position;

            // Snap the camera instantly to the new player position
            CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
            cam.SnapToPlayer(-224.5f);
            cam.SetBossZoom(25f);
            // Activate the boss
            if (boss != null)
            {
                boss.SetActive(true);
                Debug.Log("Boss activated via teleporter!");
            }
        }
    }
}