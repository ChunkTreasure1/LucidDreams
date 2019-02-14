using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] PostProcessingProfile PPProfile;
    private VignetteModel.Settings VignetteSettings;

    private float CurrTimerValue = 0;
    private float CurrTimerValueScene = 0;
    private Animator Canvas;

    private void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<Animator>();

        VignetteSettings = PPProfile.vignette.settings;
        VignetteSettings.intensity = 0;

        PPProfile.vignette.settings = VignetteSettings;
        StartCoroutine(FadeTimer());
    }

    private void Update()
    {
        VignetteSettings = PPProfile.vignette.settings;
        VignetteSettings.intensity += Time.deltaTime / 5;

        PPProfile.vignette.settings = VignetteSettings;
    }

    private IEnumerator FadeTimer(float value = 5)
    {
        CurrTimerValue = value;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrTimerValue--;
        }
        Canvas.SetBool("IsDead", true);
        StartCoroutine(SceneTimer());
    }

    private IEnumerator SceneTimer(float value = 2)
    {
        CurrTimerValueScene = value;
        while (CurrTimerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            CurrTimerValueScene--;
        }
        SceneManager.LoadScene(2);
    }
}
