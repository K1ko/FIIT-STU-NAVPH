using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainCutScene");
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit"); // For editor testing
    }
}
