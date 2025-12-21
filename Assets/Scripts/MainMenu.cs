using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour   // Manages main menu interactions and scene transitions
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

    public void OpenControls()
    {
        SceneManager.LoadScene("Controls");
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}
