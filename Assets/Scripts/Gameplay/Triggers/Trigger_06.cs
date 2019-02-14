using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_06 : MonoBehaviour
{
    [SerializeField] private StoryManager Story;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Story.SetText("I need to hide! I've probably made it angry!", true);
            Destroy(this.gameObject);
        }
    }
}
