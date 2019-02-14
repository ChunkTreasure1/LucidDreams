using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineMenu : MonoBehaviour
{
    [SerializeField] private Image ItemOne;
    [SerializeField] private Image ItemTwo;

    [SerializeField] private Sprite Battery;
    [SerializeField] private Sprite Clock;
    [SerializeField] private Sprite Flashlight;

    [SerializeField] private Sprite Hammer;
    [SerializeField] private Sprite AxeHead;
    [SerializeField] private Sprite Axe;

    [SerializeField] private Sprite File;
    [SerializeField] private Sprite Plate;
    [SerializeField] private Sprite PlateBroken;

    [SerializeField] private Sprite Broom;
    [SerializeField] private Sprite BroomStick;

    [Header("Items")]
    [SerializeField] private GameObject BrokenPlate;
    [SerializeField] private GameObject AxeHeadObj;
    [SerializeField] private GameObject BroomStickObj;
    [SerializeField] private GameObject AxeObj;

    [SerializeField] private Inventory PlayerInv;

    private bool HasFirstItem = false;
    private bool HasSecondItem = false;

    private GameObject ItemOneObj;
    private GameObject ItemTwoObj;

    enum CurrentlySelected
    {
        cS_Flashlight,
        cS_Clock,
        cS_BrokenPlate,
        cS_AxeHead,
        cS_Axe,
        cS_BroomStick,
        cS_None
    }

    CurrentlySelected CurrentlySelectedCombineItem;

    // Start is called before the first frame update
    void Start()
    {
        ItemOne.enabled = false;
        ItemTwo.enabled = false;
    }

    public void SetFlashlight()
    {
        ItemOne.sprite = Flashlight;
        ItemTwo.sprite = Battery;

        ItemOne.enabled = true;
        ItemTwo.enabled = true;

        HasFirstItem = false;
        HasSecondItem = false;

        ItemOneObj = null;
        ItemTwoObj = null;

        CurrentlySelectedCombineItem = CurrentlySelected.cS_Flashlight;
    }

    public void SetClock()
    {
        ItemOne.sprite = Clock;
        ItemTwo.sprite = Battery;

        ItemOne.enabled = true;
        ItemTwo.enabled = true;

        HasFirstItem = false;
        HasSecondItem = false;

        ItemOneObj = null;
        ItemTwoObj = null;

        CurrentlySelectedCombineItem = CurrentlySelected.cS_Clock;
    }

    public void SetBrokenPlate()
    {
        ItemOne.sprite = Hammer;
        ItemTwo.sprite = Plate;

        ItemOne.enabled = true;
        ItemTwo.enabled = true;

        HasFirstItem = false;
        HasSecondItem = false;

        ItemOneObj = null;
        ItemTwoObj = null;

        CurrentlySelectedCombineItem = CurrentlySelected.cS_BrokenPlate;
    }

    public void SetAxeHead()
    {
        ItemOne.sprite = File;
        ItemTwo.sprite = PlateBroken;

        ItemOne.enabled = true;
        ItemTwo.enabled = true;

        HasFirstItem = false;
        HasSecondItem = false;

        ItemOneObj = null;
        ItemTwoObj = null;

        CurrentlySelectedCombineItem = CurrentlySelected.cS_AxeHead;
    }

    public void SetBroomStick()
    {
        ItemOne.sprite = Hammer;
        ItemTwo.sprite = Broom;

        ItemOne.enabled = true;
        ItemTwo.enabled = true;

        HasFirstItem = false;
        HasSecondItem = false;

        ItemOneObj = null;
        ItemTwoObj = null;

        CurrentlySelectedCombineItem = CurrentlySelected.cS_BroomStick;
    }

    public void SetAxe()
    {
        ItemOne.sprite = AxeHead;
        ItemTwo.sprite = BroomStick;

        ItemOne.enabled = true;
        ItemTwo.enabled = true;

        HasFirstItem = false;
        HasSecondItem = false;

        ItemOneObj = null;
        ItemTwoObj = null;

        CurrentlySelectedCombineItem = CurrentlySelected.cS_Axe;
    }

    public void CombineItems()
    {
        if (CurrentlySelectedCombineItem == CurrentlySelected.cS_Clock)
        {
            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Clock"))
                {
                    HasFirstItem = true;
                    break;
                }
            }

            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Battery"))
                {
                    HasSecondItem = true;
                    break;
                }
            }

            if (HasSecondItem && HasFirstItem)
            {

            }
        }
        else if (CurrentlySelectedCombineItem == CurrentlySelected.cS_Flashlight)
        {
            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Flashligh"))
                {
                    HasFirstItem = true;
                    ItemOneObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Battery"))
                {
                    HasSecondItem = true;
                    ItemTwoObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            if (HasFirstItem && HasSecondItem)
            {
                ItemOneObj.GetComponent<Flashlight>().Battery = 100;
                PlayerInv.Drop(ItemTwoObj);
                Destroy(ItemTwoObj);
            }
        }
        else if (CurrentlySelectedCombineItem == CurrentlySelected.cS_BrokenPlate)
        {
            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Hammer"))
                {
                    HasFirstItem = true;
                    ItemOneObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Plate"))
                {
                    HasSecondItem = true;
                    ItemTwoObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            if (HasFirstItem && HasSecondItem)
            {
                PlayerInv.Drop(ItemTwoObj);
                Destroy(ItemTwoObj);

                GameObject bp = (GameObject)Instantiate(BrokenPlate);
                PlayerInv.PickUp(bp);
            }
        }
        else if (CurrentlySelectedCombineItem == CurrentlySelected.cS_AxeHead)
        {
            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("File"))
                {
                    HasFirstItem = true;
                    ItemOneObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("BrokenPlate"))
                {
                    HasSecondItem = true;
                    ItemTwoObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            if (HasFirstItem && HasSecondItem)
            {
                PlayerInv.Drop(ItemTwoObj);
                Destroy(ItemTwoObj);

                GameObject ah = (GameObject)Instantiate(AxeHeadObj);
                PlayerInv.PickUp(ah);
            }
        }
        else if (CurrentlySelectedCombineItem == CurrentlySelected.cS_BroomStick)
        {
            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Hammer"))
                {
                    HasFirstItem = true;
                    ItemOneObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("Broom"))
                {
                    HasSecondItem = true;
                    ItemTwoObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            if (HasFirstItem && HasSecondItem)
            {
                PlayerInv.Drop(ItemTwoObj);
                Destroy(ItemTwoObj);

                GameObject bs = (GameObject)Instantiate(BroomStickObj);
                PlayerInv.PickUp(bs);
            }
        }
        else if (CurrentlySelectedCombineItem == CurrentlySelected.cS_Axe)
        {
            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("AxeHead"))
                {
                    HasFirstItem = true;
                    ItemOneObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            for (int i = 0; i < PlayerInv.GetItems().Count; i++)
            {
                if (PlayerInv.GetItems()[i].CompareTag("BroomStick"))
                {
                    HasSecondItem = true;
                    ItemTwoObj = PlayerInv.GetItems()[i].gameObject;
                    break;
                }
            }

            if (HasFirstItem && HasSecondItem)
            {
                PlayerInv.Drop(ItemTwoObj);
                PlayerInv.Drop(ItemOneObj);
                Destroy(ItemTwoObj);
                Destroy(ItemOneObj);

                GameObject sa = (GameObject)Instantiate(AxeObj);
                PlayerInv.PickUp(sa);
            }
        }

        ItemOneObj = null;
        ItemTwoObj = null;
    }
}