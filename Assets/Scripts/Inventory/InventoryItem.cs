using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject   // Inventory item data
{
    public string itemName;
    public Sprite icon;
    public bool isStackable = true;
    public string description;
}
