using UnityEngine;

public class BookStandInteractable : MonoBehaviour, IInteractable
{
    public BookPage page;

    public void Interact()
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
        BookUIManager.instance.ShowPage(page);
    }


    public string GetInteractionPrompt()
    {
        return "Read Book (E)";
    }
}
