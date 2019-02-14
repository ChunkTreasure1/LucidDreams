using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLubricant : MonoBehaviour
{
    [SerializeField] private int Uses = 5;
    [SerializeField] private Inventory Inv;
    [SerializeField] private AudioSource Audio;
    private float LocalUses;
    private bool CanSpray = false;

    private GameObject Door;

    private void Awake()
    {
        LocalUses = Uses;
    }

    public void Spray()
    {
        if (LocalUses <= 0)
        {
            Inv.Drop(this.gameObject);
            Destroy(this.gameObject);
        }
        LocalUses -= 1;
        Door.GetComponent<DoorScript>().IsSprayed = true;
        Audio.Play();
        Inv.LeftArm.Play("Spray", 0);
    }

    private void Update()
    {
        if (Inv.GetObjectInHand() == this.gameObject)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                if (hitColliders[i].CompareTag("Door"))
                {
                    if (!hitColliders[i].GetComponent<DoorScript>().IsSprayed)
                    {
                        Inv.SetMessageText("Left click to Spray", true);
                        CanSpray = true;
                        Door = hitColliders[i].gameObject;
                        break;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && CanSpray)
            {
                Spray();
            }
        }
    }
}

