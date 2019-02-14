using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositions : MonoBehaviour
{
    [SerializeField] private List<GameObject> Batteries = new List<GameObject>();
    [SerializeField] private int AmountOfBatteries;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < AmountOfBatteries; i++)
        {
            int n = Random.Range(0, Batteries.Count);
            Batteries[n].SetActive(true);
            Batteries.Remove(Batteries[n]);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
