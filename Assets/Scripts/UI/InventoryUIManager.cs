using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
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

    void Update()
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

    public void RefreshInventory()
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
