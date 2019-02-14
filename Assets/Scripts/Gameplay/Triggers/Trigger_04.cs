using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_04 : MonoBehaviour
{
    [SerializeField] private StoryManager Story;
 
    private void OnTriggerEnter(Collider other)
    {
        Story.SetText("There's no fuse here. I'll need to find one. I believe I have one in the office.", true);
        Destroy(this.gameObject);
    }
}
