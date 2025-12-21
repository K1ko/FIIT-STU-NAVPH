using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    public InventoryItem item;
    public int quantity = 1;

    public void Interact()  // Pick up the item and add to inventory
    {
        InventoryManager.instance.AddItem(item, quantity);
        Destroy(gameObject);
        UIMessageDisplay.instance.ShowMessage($"Picked up {item.itemName} - {item.description}.");
    }

    public string GetInteractionPrompt()
    {
        return $"Pick up {item.itemName} (E)";
    }
}
