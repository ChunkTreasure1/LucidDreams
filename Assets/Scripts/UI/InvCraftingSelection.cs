using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvCraftingSelection : MonoBehaviour
{
    [SerializeField] GameObject InvPanel;
    [SerializeField] GameObject CraftPanel;

    [SerializeField] GameObject Inventory;
    [SerializeField] GameObject Crafting;

    private Image InvImage;
    private Image CraftImage;

    private bool IsInvSelected = true;

    // Start is called before the first frame update
    void Start()
    {
        InvImage = InvPanel.GetComponent<Image>();
        CraftImage = CraftPanel.GetComponent<Image>();

        InvImage.color = Color.gray;
        CraftImage.color = Color.clear;

        Inventory.SetActive(true);
        Crafting.SetActive(false);
    }

    public void SelectInventory()
    {
        if (IsInvSelected)
        {
            return;
        }
        else
        {
            IsInvSelected = true;

            InvImage.color = Color.gray;
            CraftImage.color = Color.clear;
            Crafting.SetActive(false);
            Inventory.SetActive(true);
        }
    }

    public void SelectCrafting()
    {
        if (!IsInvSelected)
        {
            return;
        }
        else
        {
            IsInvSelected = false;

            CraftImage.color = Color.gray;
            InvImage.color = Color.clear;
            Inventory.SetActive(false);
            Crafting.SetActive(true);
        }
    }
}
