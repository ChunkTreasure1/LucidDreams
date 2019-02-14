using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] private Camera Cam;
    [SerializeField] private float MaxHeadRotation = 80f;
    [SerializeField] private float MinHeadRotation = -80f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private bool InventoryOpen = false;

    private Rigidbody rb;
    private bool Grounded = true;
    private float CurrentHeadRotation = 0;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets rotation
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets rotation for the camera
    public void RotateCamera(bool _InventoryOpen)
    {
        InventoryOpen = _InventoryOpen;
    }

    //Run every physics iteration
    void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }
    }

    void PerformRotation()
    {

        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        if (Cam != null)
        {
            if (!InventoryOpen)
            {
                CurrentHeadRotation = Mathf.Clamp(CurrentHeadRotation + Input.GetAxis("Mouse Y") * 2f, MinHeadRotation, MaxHeadRotation);
                Cam.transform.localRotation = Quaternion.identity;
                Cam.transform.Rotate(Vector3.left, CurrentHeadRotation);
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        //If the player collides with the ground
        if (collision.gameObject.layer == 10)
        {
            //Set the grounded variable to true
            Grounded = true;
        }
    }
}
