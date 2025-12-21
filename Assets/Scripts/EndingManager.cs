using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingManager: MonoBehaviour   // Manages the ending sequence and transitions to main menu
{
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game quit");
    }
}
