using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafting : MonoBehaviour
{
    private List<Item> Items;
    private List<Item> DoesItHaveITem = new List<Item>();
    private bool Iscrafting = false;
    public Transform SpotToSpawn;
    public Inventory PlayerInventory;
    private bool _GoTroughRecipies;
    public Transform Player;
    [Header("Recipe")]
    public List<string> SprayCanRecipe = new List<string>();
    public List<string> KeyRecipe = new List<string>();
    [Header("Items that can be Created")]
    public GameObject SprayCan;
    public GameObject Key;

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, Player.position) <= 2)
        {
            Player.GetComponent<Inventory>().SetMessageText("Press E to Start Crafting", true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                Iscrafting = true;
            }
        }
        else
        {
            Iscrafting = false;
        }

        if(Iscrafting == true)
        {
            Debug.Log("Iscrafting");
            Player.GetComponent<Inventory>().SetMessageText("Press M to craft 5-56, Press N to craft key", true);
            if (Input.GetKeyDown(KeyCode.M))
            {
                CraftItem(SprayCanRecipe, SprayCan);
            }
            if (Input.GetKeyDown(KeyCode.N))
            {
                CraftItem(KeyRecipe, Key);
            }
        }
        else
        {
            _GoTroughRecipies = true;
            DoesItHaveITem.Clear();
        }
    }
    // To see the Inventory Of the player
    public void GetItemsInventory(List<Item> _Items)
    {
        Items = _Items;
    }

    // See if the player has a specifik part
    private void DoesItemExist(string _ItemToCheck)
    {
        for(int i = 0; i < Items.Count; i++)
        {
            if (Items[i].Name == _ItemToCheck && !DoesItHaveITem.Contains(Items[i]))
            {
                DoesItHaveITem.Add(Items[i]);
                break;
            }

        }
    }
    // See if the player have all the parts
    private void GoTRoughRecipe(List<string> Recipe)
    {
        for(int i = 0; i < Recipe.Count; i++)
        {
            DoesItemExist(Recipe[i]);
        }
    }
    // Makes the player drop and destroy every item used
    private void DropEverything()
    {
        for(int i = 0; i < DoesItHaveITem.Count; i++)
        {
            DoesItHaveITem[i].HaveBeenUsedForCrafting = true;
            PlayerInventory.Drop(DoesItHaveITem[i].gameObject);
        }
    }
    // Craft the Item
    private void CraftItem(List<string> Recipe, GameObject ItemTocreate)
    {
            if (_GoTroughRecipies == true)
            {
                GoTRoughRecipe(Recipe);
                _GoTroughRecipies = false;
            }
            Debug.Log("DoesItHaveItemCount: " + DoesItHaveITem.Count);
            if ( DoesItHaveITem.Count == Recipe.Count)
            {
                Debug.Log("It Works");
                GameObject Clone = Instantiate(ItemTocreate, SpotToSpawn.position, SpotToSpawn.rotation);
                Iscrafting = false;
                DropEverything();
            }
        
    }



}
