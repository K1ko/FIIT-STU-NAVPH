using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BookUIManager : MonoBehaviour
{
    public static BookUIManager instance;

    public GameObject bookPanel;
    public TMP_Text titleText;
    public TMP_Text contentText;
    public Button closeButton;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        closeButton.onClick.AddListener(Close);
        bookPanel.SetActive(false);
    }

    public void ShowPage(BookPage page)
    {
        titleText.text = page.title;
        contentText.text = page.content;
        bookPanel.SetActive(true);
        Time.timeScale = 0f; // Pause game while reading
    }

    public void Close()
    {
        bookPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
