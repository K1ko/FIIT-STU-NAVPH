using UnityEngine;
using TMPro;

public class UIInteractionPrompt : MonoBehaviour
{
    public static UIInteractionPrompt instance;
    public TMP_Text promptText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        SetPrompt(null);
    }

    public void SetPrompt(string prompt)
    {
        if (string.IsNullOrEmpty(prompt))
        {
            promptText.gameObject.SetActive(false);
        }
        else
        {
            promptText.text = prompt;
            promptText.gameObject.SetActive(true);
        }
    }
}
