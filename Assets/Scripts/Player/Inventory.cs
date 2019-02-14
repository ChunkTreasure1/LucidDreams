using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int MaxInvSize;
    [SerializeField] private GameObject InfoText;
    [SerializeField] private Camera Cam;

    [SerializeField] private float PickupDist = 5f;
    [SerializeField] private GameObject Hand;
    public Animator LeftArm;
    [SerializeField] private InventoryUI InvUI;

    private List<Item> Items = new List<Item>();
    private GameObject ObjectToPickup;
    private GameObject ObjectInHand;

    private GameObject InventoryPlaceholder;

    private TextMeshProUGUI MessageText;
    private int CurrentlySelected = 0;

    private bool IsReadyToPickup = false;
    private bool HandFilled = false;
    private float CurrTimerValue = 0;

    private bool TimerSet = false;
    public bool HasAxe = false;

    public Crafting Crafting;

    void Start()
    {
        MessageText = InfoText.GetComponentInChildren<TextMeshProUGUI>();
        InfoText.SetActive(false);

        InventoryPlaceholder = GameObject.Find("Inventory");
    }

    //Picks up an item
    public void PickUp(GameObject item)
    {
        AddItemToInventory(item);

        if (!HandFilled)
        {
            //If it is a flashlight
            if (item.CompareTag("Flashligh"))
            {
                LeftArm.Play("Pickup_Flash", 0);
            }
            else if (item.CompareTag("Spray"))
            {
                LeftArm.Play("Pickup_Spray", 0);
            }

            item.transform.parent = Hand.transform;

            Rigidbody rb = item.GetComponent<Rigidbody>();
            Destroy(rb);

            //Reset position
            item.transform.localPosition = Vector3.zero;
            item.transform.rotation = Quaternion.identity;
            ObjectInHand = item;
            HandFilled = true;
        }
        else
        {
            item.transform.parent = InventoryPlaceholder.transform;

            Rigidbody rb = item.GetComponent<Rigidbody>();
            Destroy(rb);

            item.transform.localPosition = Vector3.zero;
            item.transform.rotation = Quaternion.identity;
        }

        InvUI.UpdateUI();
        item.GetComponent<Item>().IsPickedUp = true;

        SetMessageText("", false);
    }

    //Drops an item
    public void Drop(GameObject item)
    {
        if (item == ObjectInHand)
        {
            ObjectInHand = null;
            HandFilled = false;
        }

        //Reset the item and remove it from the inventory
        item.transform.parent = null;
        Rigidbody rb = item.gameObject.AddComponent<Rigidbody>();

        RemoveItemFromInventory(item.GetComponent<Item>());
        this.gameObject.GetComponent<SoundSender>().SendSound(item.GetComponent<Item>().GetSoundLevel(), MovingMode.mM_Null);

        if (item.CompareTag("Spray"))
        {
            LeftArm.SetBool("HasSpray", false);
        }
        else if (item.CompareTag("Flashligh"))
        {
            LeftArm.SetBool("HasFlashlight", false);
        }

        item.GetComponent<Item>().IsPickedUp = false;
        InvUI.UpdateUI();
    }

    public List<Item> GetItems()
    {
        return Items;
    }

    //Checks for an item in the proximity
    private void CheckForItem()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Cam.transform.position, 0.2f, Cam.transform.forward, out hit, 3f))
        {
            GameObject obj = hit.collider.gameObject;
            if (obj.GetComponent<Item>() && !obj.GetComponent<Item>().IsPickedUp && obj.GetComponent<Item>().IsEnabled)
            {
                SetMessageText("Press F to pickup", true);
                IsReadyToPickup = true;

                ObjectToPickup = hit.collider.gameObject;
            }
            else
            {
                if (ObjectToPickup != null)
                {
                    SetMessageText("", false);
                    IsReadyToPickup = false;
                    ObjectToPickup = null;
                }
            }
        }
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectItem(5);
        }
    }

    private void Update()
    {
        if ((FindAxe() != CurrentlySelected) && HasAxe)
        {
            SelectItem(FindAxe());
        }

        if (ObjectInHand != null)
        {
            if (ObjectInHand.CompareTag("Spray"))
            {
                LeftArm.SetBool("HasSpray", true);
                LeftArm.SetBool("HasFlashlight", false);
                LeftArm.gameObject.transform.localPosition = new Vector3(-0.063f, -0.103f, 0.704f);

                ObjectInHand.transform.localRotation = Quaternion.Euler(22.087f, -360.43f, -272.82f);
                ObjectInHand.transform.localScale = new Vector3(0.7f, 1f, 0.7f);
                ObjectInHand.transform.localPosition = new Vector3(0.1438f, 0.0037f, 0.0248f);
            }
            else if (ObjectInHand.CompareTag("Flashligh"))
            {
                LeftArm.SetBool("HasSpray", false);
                LeftArm.SetBool("HasFlashlight", true);
                LeftArm.transform.localPosition = new Vector3(-0.129f, -0.115f, 0.808f);

                ObjectInHand.transform.localRotation = Quaternion.Euler(-171.99f, -400.89f, 7.32099f);
                ObjectInHand.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
                ObjectInHand.transform.localPosition = new Vector3(-0.0717f, -0.0399f, 0.0123f);
            }
        }
        else
        {
            LeftArm.transform.localPosition = new Vector3(-0.125f, -0.004f, 0.614f);
        }

        CheckForItem();
        CheckInput();
        //If there is an item to pick up
        if (IsReadyToPickup)
        {
            //If the player presses F
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Items.Count < MaxInvSize)
                {
                    PickUp(ObjectToPickup);
                }
            }
        }

        if (HandFilled)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Drop(ObjectInHand);
            }
        }
        // To see the Inventory Of the player
        if (Crafting)
        {
            Crafting.GetItemsInventory(Items);
        }
    }

    //Selects an item in the inventory
    private void SelectItem(int itemToSelect)
    {
        if (!HasAxe || (HasAxe && itemToSelect == FindAxe()))
        {
            if (itemToSelect > Items.Count)
            {
                SetTimer();
                return;
            }

            if (MaxInvSize > 1)
            {
                GameObject item = Items[itemToSelect - 1].gameObject;

                item.transform.parent = Hand.transform;

                //Reset transformation
                item.transform.localPosition = Vector3.zero;
                item.transform.rotation = Quaternion.identity;

                if (!HandFilled)
                {
                    ObjectInHand = item;
                    HandFilled = true;
                }
                else
                {
                    ObjectInHand.transform.parent = InventoryPlaceholder.transform;
                    ObjectInHand.GetComponent<MeshRenderer>().enabled = false;
                    //Set the UI slot to inactive
                    int index = Items.FindIndex(a => a.gameObject == ObjectInHand);
                    InvUI.GetVisibleSlots()[index].SetActive(false);

                    ObjectInHand = item;
                    item.GetComponent<MeshRenderer>().enabled = true;
                }

                if (item.CompareTag("Spray"))
                {
                    LeftArm.SetBool("HasSpray", true);
                    LeftArm.SetBool("HasFlashlight", false);
                    LeftArm.Play("Pickup_Spray", 0);
                }
                else if (item.CompareTag("Flashligh"))
                {
                    LeftArm.SetBool("HasSpray", false);
                    LeftArm.SetBool("HasFlashlight", true);
                    LeftArm.Play("Pickup_Flash", 0);
                }
                else
                {
                    LeftArm.SetBool("HasSpray", false);
                    LeftArm.SetBool("HasFlashlight", false);
                }

                CurrentlySelected = itemToSelect;
                InvUI.GetVisibleSlots()[itemToSelect - 1].SetActive(true);

                SetTimer();
            }
        }
    }

    //Adds an item to the actual inventory
    void AddItemToInventory(GameObject gameObject)
    {
        if (Items.Count < MaxInvSize)
        {
            if (gameObject.CompareTag("Axe"))
            {
                Items.Add(gameObject.GetComponentInChildren<Item>());
            }
            else
            {
                Items.Add(gameObject.GetComponent<Item>());
            }
        }
    }

    //Removes the item from the actual inventory
    public void RemoveItemFromInventory(Item item)
    {
        //Remove the item from the inventory
        Items.Remove(item);
    }

    public void SetMessageText(string text, bool enabled)
    {
        MessageText.text = text;
        InfoText.SetActive(enabled);
    }

    private IEnumerator VisibleSlotTimer(float value = 3)
    {
        CurrTimerValue = value;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrTimerValue--;
        }

        InvUI.GetVisibleSlot().SetActive(false);
        TimerSet = false;
    }

    private void SetTimer()
    {
        if (!TimerSet)
        {
            InvUI.GetVisibleSlot().SetActive(true);
            TimerSet = true;

            StartCoroutine(VisibleSlotTimer());
        }
    }

    public List<Item> ReturnItems()
    {
        return Items;
    }

    public GameObject GetObjectInHand()
    {
        return ObjectInHand;
    }

    private int FindAxe()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if (Items[i].CompareTag("Axe"))
            {
                return i;
            }
        }

        return 0;
    }

}
