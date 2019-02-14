using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorKey : MonoBehaviour
{
    private bool HasOpened = false;
    private DoorScript door;
    public Inventory inventory;
    public GameObject SpecifikDoor;

    // Update is called once per frame
    void Update()
    {

        Collider[]colliders = Physics.OverlapSphere(transform.position, 1f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Door") && colliders[i].gameObject == SpecifikDoor)
            {
                colliders[i].GetComponent<DoorScript>().DoorKey = true;
                HasOpened = true;
            }
        }

        if (HasOpened)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventory.Drop(this.gameObject);
                Destroy(gameObject);
            }
        }

    }
    
}
