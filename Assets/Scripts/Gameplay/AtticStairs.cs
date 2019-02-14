using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AtticStairs : MonoBehaviour
{
    [SerializeField] private Animator Animator;
    [SerializeField] private Animator FadeAnimator;
    [SerializeField] private Transform ExitPos;

    [SerializeField] private AudioSource Audio;

    private Inventory PlayerInventory;
    private bool HasSpray = false;
    private bool OpenAnimationDone = false;

    private float CurrTimerValue = 0;
    private int SprayIndex = 0;
    private bool TimerSet = false;

    private GameObject Player;
    private GameObject StoryManager;

    private void Start()
    {
        StoryManager = GameObject.FindGameObjectWithTag("StoryManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        //If it's the player
        if (other.CompareTag("Player"))
        {
            PlayerInventory = other.GetComponent<Inventory>();
            //Sets the message text
            PlayerInventory.SetMessageText("Press E to Enter", true);

            for (int i = 0; i < PlayerInventory.GetItems().Count; i++)
            {
                if (PlayerInventory.GetItems()[i].Name == "Spray")
                {
                    HasSpray = true;
                    SprayIndex = i;
                    break;
                }
            }

            Player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory.SetMessageText("", false);
            HasSpray = false;

            StoryManager.GetComponent<StoryManager>().SetText("", false);

            Player = null;
        }
    }

    private void Update()
    {
        if (Player != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (HasSpray)
                {
                    if (!TimerSet)
                    {
                        TimerSet = true;
                        StartCoroutine(AnimTimer());
                    }
                    Animator.SetBool("IsOpen", true);
                }
                else
                {
                    StoryManager.GetComponent<StoryManager>().SetText("It's stuck! I need to find something to make it loose.", true);
                }
            }
        }

        if (Animator.GetBool("IsOpen") && OpenAnimationDone)
        {
            FadeAnimator.SetBool("ShouldFade", true);

            if (FadeAnimator.gameObject.GetComponent<InventoryUI>().FadeAnimationDone)
            {
                FadeAnimator.SetBool("ShouldFade", false);
                Animator.SetBool("IsOpen", false);

                PlayerInventory.gameObject.transform.position = ExitPos.position;

                OpenAnimationDone = false;
                FadeAnimator.gameObject.GetComponent<InventoryUI>().FadeAnimationDone = false;

                GameObject bottle = PlayerInventory.GetItems()[SprayIndex].gameObject;
                PlayerInventory.Drop(bottle);

                Destroy(bottle);
                HasSpray = false;

                Audio.Stop();

                StoryManager.GetComponent<StoryManager>().SetText("It's so dark up here. I need to find something so I can see.", true);
            }
        }
    }

    IEnumerator AnimTimer(float value = 4)
    {
        CurrTimerValue = value;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1f);
            CurrTimerValue--;
        }

        OpenAnimationDone = true;
    }
}
