using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxeAttack : MonoBehaviour
{
    [SerializeField] GameObject NextCam;
    [SerializeField] Image BlackPanel;

    private float CurrTimerValue = 0;
    private float CurrTimerValueNext = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BlackTimer(1f));
    }

    private IEnumerator BlackTimer(float value = 3)
    {
        CurrTimerValue = value;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrTimerValue--;
        }

        BlackPanel.enabled = true;
        StartCoroutine(NextTimer(1f));
    }

    private IEnumerator NextTimer(float value = 3)
    {
        CurrTimerValueNext = value;
        while (CurrTimerValueNext > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrTimerValueNext--;
        }

        NextCam.SetActive(true);
        BlackPanel.enabled = false;
        gameObject.SetActive(false);
    }
}
