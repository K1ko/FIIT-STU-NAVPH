using UnityEngine;

[CreateAssetMenu(menuName = "Story/Book Page")]
public class BookPage : ScriptableObject    // Represents a memory fragment
{
    public string title;
    [TextArea(5, 15)]
    public string content;
}
