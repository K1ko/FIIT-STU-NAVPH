using UnityEngine;

public class ChestInteractable : MonoBehaviour, IInteractable
{
    public InventoryItem requiredKey;
    public Animator animator;
    public bool isOpened = false;
    public InventoryItem lootItem;

    public GameObject lootPickupPrefab; 
    public Transform dropPoint;       


    public void Interact()
    {
        if (isOpened) return;

        if (InventoryManager.instance.UseItem(requiredKey))
        {
            isOpened = true;
            if (animator != null)
                animator.SetTrigger("Open");

            UIMessageDisplay.instance.ShowMessage("You unlocked the chest!");

            if (lootItem != null && lootPickupPrefab != null)
            {
                Vector3 spawnPosition = dropPoint != null ? dropPoint.position : transform.position + Vector3.up;

                GameObject pickupObj = Instantiate(lootPickupPrefab, spawnPosition, Quaternion.identity);

                ItemPickup pickup = pickupObj.GetComponent<ItemPickup>();
                if (pickup != null)
                {
                    pickup.item = lootItem;
                    pickup.quantity = 1; // or customize if needed
                }
            }

        } else
        {
            UIMessageDisplay.instance.ShowMessage("You need a key to open this chest.");
        }
    }

    public string GetInteractionPrompt()
    {
        if (isOpened)
            return null;

        return "Open Chest (E)";
    }
}
