using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class BookUIManager : MonoBehaviour  // Manages the UI for displaying book pages
{
    public static BookUIManager instance;

    public GameObject bookPanel;
    public TMP_Text titleText;
    public TMP_Text contentText;
    public Button closeButton;

    private Action onCloseCallback;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        closeButton.onClick.AddListener(Close);
        bookPanel.SetActive(false);
    }

    public void ShowPage(BookPage page, Action onClose = null)
    // Displays the book page UI with title and content
    {
        titleText.text = page.title;
        contentText.text = page.content;
        bookPanel.SetActive(true);
        Time.timeScale = 0f;

        onCloseCallback = onClose; // Capture the callback
    }

    public void Close()
    {
        bookPanel.SetActive(false);
        Time.timeScale = 1f;

        onCloseCallback?.Invoke(); // Invoke the callback if assigned
        onCloseCallback = null;    // Clear after use
    }
}
