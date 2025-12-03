using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public InventoryItem item;
    public int quantity = 1;

    public void Interact()
    {
        InventoryManager.instance.AddItem(item, quantity);
        Destroy(gameObject);
        UIMessageDisplay.instance.ShowMessage($"Picked up {item.itemName}");
    }

    public string GetInteractionPrompt()
    {
        return $"Pick up {item.itemName} (E)";
    }
}
