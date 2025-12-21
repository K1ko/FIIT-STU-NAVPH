using UnityEngine;

public class BookStandInteractable : MonoBehaviour, IInteractable
{
    public BookPage page;

    protected System.Action onReadComplete;

    public void Interact()
    //Handles interaction with the BookStand.
    //Displays the book page and adds entry to journal.
    {
        Debug.Log("Interacting with BookStand...");

        if (page == null)
        {
            Debug.LogError("BookPage is not assigned!");
            return;
        }

        if (JournalUIManager.instance == null)
        {
            Debug.LogError("JournalUIManager.instance is NULL!");
            return;
        }

        Debug.Log("Adding entry to Journal...");
        JournalUIManager.instance.AddEntry(page.title, page.content);
        StoryLogManager.instance.UnlockPage(page);

        // Invoke callback after reading the book
        BookUIManager.instance.ShowPage(page, onReadComplete);
    }

    public void SetOnReadComplete(System.Action callback)
    // Sets the callback to be invoked after the book is read
    {
        onReadComplete = callback;
    }

    public string GetInteractionPrompt()
    {
        return "Read Book (E)";
    }
}
