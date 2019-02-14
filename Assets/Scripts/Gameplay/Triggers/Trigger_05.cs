using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_05 : MonoBehaviour
{
    [SerializeField] private StoryManager Story;
    [SerializeField] private GameObject NextTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Story.SetText("Found it! Now I just need to replace it.", true);
            NextTrigger.SetActive(true);

            Destroy(this.gameObject);
        }
    }
}
