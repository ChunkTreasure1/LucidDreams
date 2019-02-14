using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float SoundLevel = 100f;
    public Animator _animate;
    [SerializeField] private SoundSender SoundSender;

    [Header("Audio")]
    [SerializeField] private AudioSource OpenDoor;
    [SerializeField] private AudioSource CreakingDoor;
    [SerializeField] private AudioSource HandleShake;

    public bool IsLocked;
    public bool CanOpen;
    public bool DoorKey;

    public bool IsSprayed = false;
    private bool Pressed = false;
    private Inventory PlayerInv;

    // Start is called before the first frame update
    void Start()
    {
        _animate = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {           
         if (other.tag == "Player")
         {
            CanOpen = true;
            PlayerInv = other.GetComponent<Inventory>();
            PlayerInv.SetMessageText("Press E to open", true);
            Pressed = false;
         }     

    }

    private void OnTriggerExit(Collider other)
    {               
         if (other.tag == "Player")
         {
            CanOpen = false;
            PlayerInv.SetMessageText("", false);
            PlayerInv = null;
            Pressed = false;
         }       
    }

    // Update is called once per frame
    void Update()
    {   
        if(CanOpen)
        {
            if (IsLocked)
            {
                if (!DoorKey)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        HandleShake.Play();
                        PlayerInv.SetMessageText("Door locked, key needed.", false);
                    }
                }
                if (DoorKey)
                {
                    if(Input.GetKeyDown(KeyCode.E))
                    {
                        OpenDoor.Play();

                        if (IsSprayed)
                        {
                            SoundSender.SendSound(0, MovingMode.mM_Null);
                        }
                        else
                        {
                            CreakingDoor.Play();
                            SoundSender.SendSound(SoundLevel, MovingMode.mM_Null);
                        }

                        CanOpen = false;
                        IsLocked = false;

                        if (!Pressed)
                        {
                            Pressed = true;
                            _animate.SetBool("open", !_animate.GetBool("open"));
                            PlayerInv.SetMessageText("", false);
                        }
                    }
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OpenDoor.Play();

                    if (IsSprayed)
                    {
                        SoundSender.SendSound(0, MovingMode.mM_Null);
                    }
                    else
                    {
                        CreakingDoor.Play();
                        SoundSender.SendSound(SoundLevel, MovingMode.mM_Null);
                    }
                    IsLocked = false;
                    CanOpen = false;

                    if (!Pressed)
                    {
                        Pressed = true;
                        _animate.SetBool("open", !_animate.GetBool("open"));
                        PlayerInv.SetMessageText("", false);
                    }
                }
            }
        }

    }
        
}
