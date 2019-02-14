using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    [SerializeField] private float MiniTime;
    [SerializeField] private float MaxTime;
    [SerializeField] private float CurrentTime;
    [SerializeField] private float StartLightningTime;
    private float LightningTime;
    private Light _light;
    private AudioSource LightningSound;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        LightningSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
        }
        else
        {
            CurrentTime = Random.Range(MiniTime, MaxTime);
            LightningTime = StartLightningTime;
        }

        if(LightningTime > 0)
        {
            _light.enabled = true;
            LightningTime -= Time.deltaTime;
            LightningSound.Play();
        }
        else
        {
            _light.enabled = false;
        }
    }
}
