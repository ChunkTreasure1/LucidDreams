using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fade : MonoBehaviour
{
    public void FadeScreen()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        StartCoroutine("DoFade");
        canvasGroup.alpha = 0;
    }

    IEnumerable DoFade()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.deltaTime / 2;
            yield return null;
        }
        canvasGroup.interactable = false;
        yield return null;
    }
}
