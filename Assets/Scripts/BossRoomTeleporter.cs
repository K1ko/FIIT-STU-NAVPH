using UnityEngine;

public class BossRoomTeleporter : MonoBehaviour
{
    public Transform teleportPoint;
    public GameObject boss;

    private bool used = false;
    public AudioSource teleportPlayer;
    public AudioClip teleportSound;
    private void Start()
    {
        // Get the BookStandInteractable component on last book
        BookStandInteractable bookStand = GetComponent<BookStandInteractable>();

        if (bookStand == null)
        {
            Debug.LogError("BossRoomTeleporter requires a BookStandInteractable!");
            return;
        }

        // After reading the book, teleport player
        bookStand.SetOnReadComplete(() =>
        {
            if (used) return;
            used = true;
            PlayTeleportSound();
            TeleportPlayer();
        });
    }

    private void TeleportPlayer()
    // Teleport player to boss room
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        player.transform.position = teleportPoint.position;

        CameraFollow cam = Camera.main.GetComponent<CameraFollow>();
        cam.SetBossBounds();
        cam.SnapToPlayer(-224.65f);
        cam.SetBossZoom(22f);

        if (boss != null)
        {
            boss.SetActive(true);
            Debug.Log("Boss activated after reading final book!");
        }
    }

    public void PlayTeleportSound()
    {
        if (teleportSound != null && teleportPlayer != null)
        {
            teleportPlayer.PlayOneShot(teleportSound);
        }
    }
    
}
