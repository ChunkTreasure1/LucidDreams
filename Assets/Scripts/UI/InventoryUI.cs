using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Inventory Inventory;
    [SerializeField] private GameObject InventoryUIMain;
    [SerializeField] private GameObject VisibleSlots;
    [SerializeField] private Image BigImage;
    [SerializeField] private TextMeshProUGUI Tooltip;

    [Header("Parents")]
    [SerializeField] private Transform ItemsParent;
    [SerializeField] private Transform ItemsParentSelectionSlot;

    private InventorySlot[] Slots;
    private SelectionSlot[] SelectionSlots;
    private bool InventoryOpen = false;

    public bool FadeAnimationDone = false;
    private bool StartFade = false;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().speed = 0.5f;
        gameObject.GetComponent<Animator>().SetBool("IsStart", true);
        Slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
        SelectionSlots = ItemsParentSelectionSlot.GetComponentsInChildren<SelectionSlot>();

        InventoryUIMain.SetActive(false);
        VisibleSlots.SetActive(false);
        InventoryOpen = false;

        BigImage.enabled = false;
        Tooltip.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //If the player presses tab
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryUIMain.SetActive(!InventoryUIMain.activeSelf);
            InventoryOpen = InventoryUIMain.activeSelf;
        }

        if (StartFade)
        {
            gameObject.GetComponent<Animator>().SetBool("IsStart", false);
            gameObject.GetComponent<Animator>().speed = 1f;
            StartFade = false;
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i < Inventory.GetItems().Count)
            {
                Slots[i].AddItem(Inventory.GetItems()[i]);
                SelectionSlots[i].AddItem(Inventory.GetItems()[i]);
            }
            else
            {
                Slots[i].ClearSlot();
                if (i < SelectionSlots.Length)
                {
                    SelectionSlots[i].ClearSlot();
                }
            }
        }
    }

    public bool GetInventoryOpen()
    {
        return InventoryOpen;
    }

    public void AnimationFinished()
    {
        FadeAnimationDone = true;
    }

    public SelectionSlot[] GetVisibleSlots()
    {
        return SelectionSlots;
    }

    public GameObject GetVisibleSlot()
    {
        return VisibleSlots;
    }

    public void StartFadeDone()
    {
        StartFade = true;
    }
}
