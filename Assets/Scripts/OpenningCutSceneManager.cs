using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpenningCutSceneManager : MonoBehaviour    // Manages the opening cutscene and transitions to the main game scene
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; // Event when video finishes
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("GameScene");
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}