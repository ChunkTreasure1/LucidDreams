using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public GameObject Movmentmanager;
    public float Sound;
    [SerializeField]
    private float TimerBeforestart;
    [SerializeField]
    private float TimeWhenringing;

    private Animator Animator;
    private AudioSource AudioSource;
    private SoundSender SoundSender;

    private bool IsRinging = false;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        AudioSource = GetComponent<AudioSource>();
        SoundSender = GetComponent<SoundSender>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && transform.parent)
        {
            IsRinging = true;
            gameObject.AddComponent<Rigidbody>();
            transform.parent = null;
        }
        if(transform.parent == null)
        {
            if (TimerBeforestart > 0)
            {
                TimerBeforestart -= Time.deltaTime;
            }
            else if (TimerBeforestart <= 0 && TimeWhenringing > 0)
            {
                StartRinging();
                SoundSender.SendSound(Sound, MovingMode.mM_Null);
                TimeWhenringing -= Time.deltaTime;
            }
            else if (TimeWhenringing <= 0)
            {
                StopRinging();
            }
        }
    }

    void StartRinging()
    {
        if (IsRinging)
        {
            IsRinging = false;
            AudioSource.Play();
            Animator.SetBool("IsRinging", true);
        }
    }

    void StopRinging()
    {
        AudioSource.Stop();
        Animator.SetBool("IsRinging", false);
    }
}
