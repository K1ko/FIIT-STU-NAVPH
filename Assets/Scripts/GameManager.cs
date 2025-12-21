using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour    // Manages game state and collected items for ending conditions
{
    public static GameManager Instance;

    public int booksCollected = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Destroy(gameObject);
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
