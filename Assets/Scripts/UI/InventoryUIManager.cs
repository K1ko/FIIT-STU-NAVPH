using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour // Manages the inventory UI
{
    public static InventoryUIManager instance;

    public GameObject inventoryPanel;
    public Transform itemGrid;
    public GameObject itemSlotPrefab;

    private bool isVisible = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        inventoryPanel.SetActive(false);
    }

    void Update()   // Toggle inventory with "I" key
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
    }

    public void ToggleInventory()
    {
        isVisible = !isVisible;
        inventoryPanel.SetActive(isVisible);
        if (isVisible)
        {
            RefreshInventory();
        }
    }

    public void RefreshInventory()  // Update the inventory UI display
    {
        foreach (Transform child in itemGrid)
        {
            Destroy(child.gameObject);
        }

        foreach (var slot in InventoryManager.instance.GetInventory())
        {
            GameObject go = Instantiate(itemSlotPrefab, itemGrid);
            go.transform.Find("Icon").GetComponent<Image>().sprite = slot.item.icon;
            go.transform.Find("Quantity").GetComponent<TMP_Text>().text = slot.item.isStackable ? slot.quantity.ToString() : "";
        }
    }
}
