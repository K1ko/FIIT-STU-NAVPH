using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JournalUIManager : MonoBehaviour
{
    public static JournalUIManager instance;

    [Header("UI References")]
    public GameObject journalPanel;
    public GameObject entryButtonPrefab;
    public Transform entryListParent;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI contentText;
    public Button backButton; // <-- Reference to your Back Button

    private List<JournalEntry> journalEntries = new();

    [System.Serializable]
    public class JournalEntry
    {
        public string title;
        public string content;

        public JournalEntry(string title, string content)
        {
            this.title = title;
            this.content = content;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ToggleJournal();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        journalPanel.SetActive(false);

        // Hook up the back button once
        if (backButton != null)
        {
            backButton.onClick.AddListener(CloseJournal);
        }
        else
        {
            Debug.LogWarning("Back button is not assigned in JournalUIManager.");
        }
    }

    public void AddEntry(string title, string content)
    {
        // Check if an entry with the same title already exists
        foreach (var entry in journalEntries)
        {
            if (entry.title == title) return; // Don't add duplicate
        }

        // Add new entry
        var newEntry = new JournalEntry(title, content);
        journalEntries.Add(newEntry);

        GameObject buttonGO = Instantiate(entryButtonPrefab, entryListParent);
        TextMeshProUGUI label = buttonGO.GetComponentInChildren<TextMeshProUGUI>();
        label.text = title;

        Button button = buttonGO.GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            BookPage tempPage = ScriptableObject.CreateInstance<BookPage>();
            tempPage.title = newEntry.title;
            tempPage.content = newEntry.content;

            journalPanel.SetActive(false);
            BookUIManager.instance.ShowPage(tempPage);
        });
    }


    public void ShowEntry(JournalEntry entry)
    {
        titleText.text = entry.title;
        contentText.text = entry.content;
    }

    public void ToggleJournal()
    {
        journalPanel.SetActive(!journalPanel.activeSelf);
    }

    public void CloseJournal()
    {
        journalPanel.SetActive(false);
    }
}
