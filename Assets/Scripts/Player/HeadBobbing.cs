using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour
{
    [SerializeField] private float BobbingSpeed = 0.2f;
    [SerializeField] [Range(0, 1.5f)] private float BobbingAmount = 0.2f;
    [SerializeField] private float Midpoint = 2f;

    private float Timer = 0f;

    private void Update()
    {
        float waveslice = 0f;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 pos = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            Timer = 0f;
        }
        else
        {
            waveslice = Mathf.Sin(Timer);
            Timer = Timer + BobbingSpeed;

            if (Timer > Mathf.PI * 2)
            {
                Timer = Timer - (Mathf.PI * 2);
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * BobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);

            totalAxes = Mathf.Clamp(totalAxes, 0f, 1f);
            translateChange = totalAxes * translateChange;

            pos.y = Midpoint + translateChange;

        }

        transform.localPosition = pos;
    }
}
