using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlsManager : MonoBehaviour    // Manages the controls screen and returns to main menu
{
    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
