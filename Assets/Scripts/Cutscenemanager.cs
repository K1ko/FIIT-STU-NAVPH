using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cutscenemanager : MonoBehaviour    // Manages cutscene playback and transitions
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += OnVideoEnd; // Event when video finishes
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene("MainMenu");
    }
    void Update()
    {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
