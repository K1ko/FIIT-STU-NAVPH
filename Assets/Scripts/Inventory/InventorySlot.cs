[System.Serializable]
public class InventorySlot  // Represents a slot in the inventory
{
    public InventoryItem item;
    public int quantity;

    public InventorySlot(InventoryItem item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }

    public void AddQuantity(int amount)
    {
        quantity += amount;
    }
}
