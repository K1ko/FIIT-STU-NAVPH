using UnityEngine;

[CreateAssetMenu(menuName = "Story/Book Page")]
public class BookPage : ScriptableObject
{
    public string title;
    [TextArea(5, 15)]
    public string content;
}
