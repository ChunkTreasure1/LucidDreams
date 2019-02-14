using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float SpeedBoostValue;
    public bool BoostExist = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float Speed_Boost(float Speed)
    {
        if (BoostExist)
        {
            Speed += SpeedBoostValue;
            BoostExist = false;
        }

        return Speed;
    }
}
