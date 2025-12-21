using UnityEngine;
using TMPro;

public class UIMessageDisplay : MonoBehaviour   // Manages on-screen message displays
{
    public static UIMessageDisplay instance;

    public TMP_Text messageText;
    public float messageDuration = 2f;

    private Coroutine messageCoroutine;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        messageText.gameObject.SetActive(false);
    }

    public void ShowMessage(string message) // Displays a message for a set duration
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(DisplayMessage(message));
    }

    public void ShowMessageBoss(string message) // Displays a boss message for a longer duration
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(DisplayMessageBoss(message));
    }

    private System.Collections.IEnumerator DisplayMessage(string message)   // Coroutine to handle message display timing
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        messageText.gameObject.SetActive(false);
    }
    private System.Collections.IEnumerator DisplayMessageBoss(string message)   // Coroutine to handle boss message display timing
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(10f);

        messageText.gameObject.SetActive(false);
    }
}
