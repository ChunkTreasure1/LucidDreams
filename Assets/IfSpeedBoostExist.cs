using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfSpeedBoostExist : MonoBehaviour
{
    public GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Player.GetComponent<SpeedBoost>().BoostExist = true;
        if (Input.GetKey(KeyCode.Mouse0))
        {            
            Destroy(gameObject);
        }
    }
}
