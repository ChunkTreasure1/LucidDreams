using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_02 : MonoBehaviour
{
    [SerializeField] private StoryManager Story;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Story.SetText("I need to call for help. There is a telecom somwhere, I can use that", true);
        }
        Destroy(this.gameObject);
    }
}
