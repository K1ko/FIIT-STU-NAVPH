using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int booksCollected = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectBook()
    {
        booksCollected++;
    }

    public int GetBooks()
    {
        return booksCollected;
    }
}
