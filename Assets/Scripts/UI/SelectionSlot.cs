using UnityEngine;
using UnityEngine.UI;

public class SelectionSlot : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] Inventory Inventory;
    [SerializeField] private Image SelectionPanel;

    private Item Item;

    //Adds an item to the slot
    public void AddItem(Item newItem)
    {
        Item = newItem;

        Icon.sprite = Item.GetImage();
        Icon.enabled = true;
    }

    //Clears the slot
    public void ClearSlot()
    {
        Item = null;

        Icon.sprite = null;
        Icon.enabled = false;
    }

    public void OnRemoveButton()
    {
        Inventory.Drop(Item.gameObject);
    }

    public void UseItem()
    {
        if (Item != null)
        {
            Item.Use();
        }
    }

    public void SetActive(bool state)
    {
        SelectionPanel.enabled = state;
    }
}
