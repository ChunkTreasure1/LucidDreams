using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuzeBoxTrigger : MonoBehaviour
{
    [SerializeField] private StoryManager Story;
    [SerializeField] private GameObject Door;
    [SerializeField] private GameObject[] Parts;

    [Header("GetAway Scene")]
    [SerializeField] private GameObject GetAwayScene;
    [SerializeField] private Transform InstatiatePoint;
    [SerializeField] private GameObject Enemy;

    public bool IsEnabled = false;
    private bool InTrigger = false;
    private GameObject Player;
    private GameObject Anim;
    private bool FirstTime = true;

    private float CurrTimerValue = 0;
    private float CurrTimerValueAnim = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && IsEnabled)
        {
            InTrigger = true;
            Player = other.gameObject;

            other.GetComponent<Inventory>().SetMessageText("Press E to interact", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && IsEnabled)
        {
            InTrigger = false;
            Player = null;

            other.GetComponent<Inventory>().SetMessageText("", false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && InTrigger && FirstTime)
        {
            Player.GetComponent<Inventory>().SetMessageText("", false);

            if (HasScrewdriver(Player))
            {
                Door.SetActive(false);

                Player.GetComponent<Inventory>().SetMessageText("Press E to insert fuse", true);
                FirstTime = false;
            }
            else
            {
                Story.SetText("I can't open it without a screwdriver", true);
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && InTrigger && !FirstTime)
        {
            Player.GetComponent<Inventory>().SetMessageText("", false);

            if (HasFuse(Player))
            {
                Anim = Instantiate(GetAwayScene, InstatiatePoint.position, InstatiatePoint.rotation);
                Player.SetActive(false);
                Enemy.SetActive(false);
                StartCoroutine(GetAway());
            }
        }
    }

    private bool HasScrewdriver(GameObject player)
    {
        for (int i = 0; i < player.GetComponent<Inventory>().GetItems().Count; i++)
        {
            if (player.GetComponent<Inventory>().GetItems()[i].CompareTag("Screwdriver"))
            {
                return true;
            }
        }

        return false;
    }

    private bool HasFuse(GameObject player)
    {
        for (int i = 0; i < player.GetComponent<Inventory>().GetItems().Count; i++)
        {
            if (player.GetComponent<Inventory>().GetItems()[i].CompareTag("Fuse"))
            {
                return true;
            }
        }

        return false;
    }

    private void AfterAttack()
    {
        Story.SetText("I need to get out of here and hide!", true);
        StartCoroutine(StoryTimer());
    }

    private IEnumerator StoryTimer(float value = 10)
    {
        CurrTimerValue = value;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrTimerValue--;
        }

        for (int i = 0; i < Parts.Length; i++)
        {
            Parts[i].GetComponentInChildren<Item>().IsEnabled = true;
        }
        Story.SetText("I need to make a weapon. We have to have something I can use!", true);
    }

    private IEnumerator GetAway(float value = 4.5f)
    {
        CurrTimerValueAnim = value;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrTimerValueAnim--;
        }

        Player.SetActive(true);
        Enemy.SetActive(true);
        Destroy(Anim);
        AfterAttack();
    }
}
