using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    private List<InventorySlot> inventory = new List<InventorySlot>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(InventoryItem item, int quantity = 1)
    {
        InventorySlot slot = inventory.Find(s => s.item == item);

        if (slot != null && item.isStackable)
        {
            slot.AddQuantity(quantity);
        }
        else
        {
            inventory.Add(new InventorySlot(item, quantity));
        }

        if (item.itemName == "Jordany")
        {
        PlayerMovementPlatformer player =
            FindFirstObjectByType<PlayerMovementPlatformer>();

        if (player != null)
            player.EnableDoubleJump();
        }


        Debug.Log($"Added {item.itemName} x{quantity} to inventory");
    }

    public bool HasItem(InventoryItem item)
    {
        return inventory.Exists(slot => slot.item == item);
    }

    public bool UseItem(InventoryItem item)
    {
        InventorySlot slot = inventory.Find(s => s.item == item);
        if (slot != null && slot.quantity > 0)
        {
            slot.quantity--;
            if (slot.quantity <= 0)
                inventory.Remove(slot);
            return true;
        }

        return false;
    }

    public List<InventorySlot> GetInventory()
    {
        return inventory;
    }
}
