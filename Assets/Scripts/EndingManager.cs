using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager: MonoBehaviour
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit"); // For editor testing
    }
}
