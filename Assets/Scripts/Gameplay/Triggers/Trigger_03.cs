using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_03 : MonoBehaviour
{
    [SerializeField] private StoryManager Story;
    [SerializeField] private Light[] Lights;
    [SerializeField] private SearchTrigger[] Triggers;
    [SerializeField] private FuzeBoxTrigger FuzeBox;

    private bool InTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = true;
            other.GetComponent<Inventory>().SetMessageText("Press E to use", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = false;
            other.GetComponent<Inventory>().SetMessageText("", false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && InTrigger)
        {
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i].enabled = false;
            }

            for (int i = 0; i < Triggers.Length; i++)
            {
                Triggers[i].IsEnabled = true;
            }

            FuzeBox.IsEnabled = true;
            Story.SetText("Damn! The fuse went, I'll need to replace it. I believe there's one in the storage.", true);
            Destroy(this);
        }
    }
}
