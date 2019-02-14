using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light Light;
    public float Battery;

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Item>().IsPickedUp && GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().GetObjectInHand() == this.gameObject)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Light.enabled = !Light.enabled;
            }
        }
        else
        {
            Light.enabled = false;
        }

        if(Battery >= 0 && Light.enabled == true)
        {
            Battery -= Time.deltaTime;
        }
        if(Battery <= 0)
        {
            Light.enabled = false;
        }
    }
}
