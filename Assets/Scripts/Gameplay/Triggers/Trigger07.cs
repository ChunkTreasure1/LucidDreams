using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger07 : MonoBehaviour
{
    [SerializeField] private StoryManager Story;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Story.SetText("Press CTRL to crouch", true);
        }
        Destroy(this.gameObject);
    }
}
