using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OpenningCutSceneManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; // Event when video finishes
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("GameScene"); // Change if your scene is named differently
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}