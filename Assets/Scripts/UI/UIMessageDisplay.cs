using UnityEngine;
using TMPro;

public class UIMessageDisplay : MonoBehaviour
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

    public void ShowMessage(string message)
    {
        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        messageCoroutine = StartCoroutine(DisplayMessage(message));
    }

    private System.Collections.IEnumerator DisplayMessage(string message)
    {
        messageText.text = message;
        messageText.gameObject.SetActive(true);

        yield return new WaitForSeconds(messageDuration);

        messageText.gameObject.SetActive(false);
    }
}
