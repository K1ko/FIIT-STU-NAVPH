using System.Collections.Generic;
using UnityEngine;

public class StoryLogManager : MonoBehaviour
{
    public static StoryLogManager instance;

    private List<BookPage> unlockedPages = new List<BookPage>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void UnlockPage(BookPage page)
    {
        if (!unlockedPages.Contains(page))
        {
            unlockedPages.Add(page);
            GameManager.Instance.CollectBook();
            Debug.Log("Story page unlocked: " + page.title);
        }
    }

    public List<BookPage> GetUnlockedPages()
    {
        return unlockedPages;
    }
}
