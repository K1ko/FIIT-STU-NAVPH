using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string promptText = "Press E to test this!";

    public void Interact()
    {
        Debug.Log("Test object interacted!");
    }

    public string GetInteractionPrompt()
    {
        return promptText;
    }
}
