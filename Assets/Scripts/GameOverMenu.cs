using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour   // Manages the game over menu options
{
    public void RestartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
