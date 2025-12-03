using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    public InventoryItem requiredKey;
    public Animator animator;
    public bool isOpened = false;

    public void Interact()
    {
        if (isOpened) return;

        if (InventoryManager.instance.UseItem(requiredKey))
        {
            isOpened = true;
            if (animator != null)
                animator.SetTrigger("Open");

            UIMessageDisplay.instance.ShowMessage("You unlocked the chest!");
            // loot here 
        }
        else
        {
            UIMessageDisplay.instance.ShowMessage("Chest is locked. You need a key.");
        }
    }

    public string GetInteractionPrompt()
    {
        if (isOpened)
            return null;

        return "Open Chest (E)";
    }
}
