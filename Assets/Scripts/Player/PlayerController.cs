using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float RunningSpeed = 8f;
    [SerializeField] private float WalkSpeed = 5f;
    [SerializeField] private float CrouchedSpeed = 3f;

    [Header("Sensitivity")]
    [SerializeField] private float LookSensitivity = 3f;

    [Header("Sound")]
    [SerializeField] [Range(0, 1)] private float RunSoundLevel = 0.7f;
    [SerializeField] [Range(0, 1)] private float WalkSoundLevel = 0.5f;
    [SerializeField] [Range(0, 1)] private float CrouchSoundLevel = 0.3f;

    [Header("Other")]
    [SerializeField] private SoundSender SoundSender;
    [SerializeField] private Animator LeftArm;
    [SerializeField] private Animator RightArm;
    [SerializeField] private GameObject AxeArms;
    [SerializeField] private Camera Cam;

    [SerializeField] private InventoryUI InventoryUI;
    [SerializeField] private Inventory Inventory;

    private float Speed;
    private bool Crouched = false;
    private bool IsRunning = false;

    private bool ShouldJump = false;
    private PlayerMotor Motor;
    private SpeedBoost SpeedBoost;

    private bool GiveBoost = true;
    public float BoostTimer;
    public float StartBoostTimer;

    private float RunSpeedValue;
    private float WalkSpeedValue;
    private float CrouchSpeedValue;

    public bool CanMove = true;
    private PlayerDeath DeathItem;

    private CapsuleCollider Collider;

    // Start is called before the first frame update
    void Start()
    {
        RunSpeedValue = RunningSpeed;
        WalkSpeedValue = WalkSpeed;
        CrouchSpeedValue = CrouchedSpeed;

        Motor = GetComponent<PlayerMotor>();
        SpeedBoost = GetComponent<SpeedBoost>();
        Collider = GetComponent<CapsuleCollider>();

        if (GameObject.Find("Death"))
        {
            DeathItem = GameObject.Find("Death").GetComponent<PlayerDeath>();
        }

        AxeArms.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Inventory.HasAxe)
        {
            AxeArms.SetActive(true);
            RightArm.gameObject.SetActive(false);
            LeftArm.gameObject.SetActive(false);
        }

        if (Input.GetKey(KeyCode.Mouse0) && GiveBoost)
        {
            if (SpeedBoost != null)
            {
                RunSpeedValue = GetComponent<SpeedBoost>().Speed_Boost(RunningSpeed);
                WalkSpeedValue = GetComponent<SpeedBoost>().Speed_Boost(WalkSpeed);
                CrouchSpeedValue = GetComponent<SpeedBoost>().Speed_Boost(CrouchedSpeed);

                GiveBoost = false;
            }
        }
        if (!GiveBoost)
        {
            if(BoostTimer > 0)
            {
                BoostTimer -= Time.deltaTime;
            }
            else
            {
                RunSpeedValue = RunningSpeed;
                WalkSpeedValue = WalkSpeed;
                CrouchSpeedValue = CrouchedSpeed;

                BoostTimer = StartBoostTimer;
                GiveBoost = true;
            }
        }
        CheckInputs();

        //Calculate movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        //Start the walk animation
        LeftArm.SetFloat("Speed", Mathf.Abs(_xMov + _zMov));
        RightArm.SetFloat("Speed", Mathf.Abs(_xMov + _zMov));

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;

        SetSpeed();

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * Speed;

        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * LookSensitivity;

        if (InventoryUI.GetInventoryOpen() || !CanMove)
        {
            _velocity = Vector3.zero;
            _rotation = Vector3.zero;
        }

        Motor.RotateCamera(InventoryUI.GetInventoryOpen());
        Motor.Rotate(_rotation);
        Motor.Move(_velocity);
    }

    private void CheckInputs()
    {
        //If the player has pressed the left control button
        //set crouched to what it's not
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //Sets crouched to what it's not and gives that value to the animator
            Crouched = !Crouched;

            if (Crouched == false)
            {
                Collider.height = 2f;
            }
            else
            {
                Collider.height = 1.5f;
            }

            Debug.Log("Crouched");        
        }

        //Gets the current "speed" the player
        float speed = LeftArm.GetFloat("Speed");

        //If the player presses the left shift button, make the player run
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            //Set the values
            IsRunning = true;
            Crouched = false;

            //Make the running animation play

        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            //When the player releases the shift button stop the running
            IsRunning = false;

        }

        //If the player is pressing the shift button but is
        //standing still, make sure that the enemy idle animation is playing
        if (IsRunning && speed <= 0.01f )
        {

        }
    }

    private void SetSpeed()
    {
        if (!Inventory.HasAxe)
        {
            //Checks if the player is crouched, if it is set the speed to the set crouched speed
            if (Crouched)
            {
                Speed = CrouchSpeedValue;
                SoundSender.SendSound(CrouchSoundLevel, MovingMode.mM_Crouched);
            }
            //Check if the player is pressing the shift button and want's to run
            else if (IsRunning && !Crouched)
            {
                //Sets the speed to the set running speed
                Speed = RunSpeedValue;
                SoundSender.SendSound(RunSoundLevel, MovingMode.mM_Walk);
            }
            else if (!IsRunning && !Crouched)
            {
                //Otherwise set the speed to the walking speed
                Speed = WalkSpeedValue;
                SoundSender.SendSound(WalkSoundLevel, MovingMode.mM_Walk);
            }
        }
    }
}

namespace UnityEngine
{
    public enum MovingMode
    {
        mM_Run,
        mM_Walk,
        mM_Crouched,
        mM_Null
    }
}
