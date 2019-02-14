using UnityEngine;
using UnityEngine.UI;
using TMPro;

class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image Icon;
    [SerializeField] private Inventory Inventory;

    [SerializeField] private Image BigImage;
    [SerializeField] private TextMeshProUGUI ToolTipText;
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

    public void UseItem()
    {
        if (Item != null)
        {
            BigImage.sprite = Icon.sprite;
            ToolTipText.text = Item.GetToolTip();
            BigImage.enabled = true;
            ToolTipText.enabled = true;
            Item.Use();
        }
    }
}
