using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPhoto : MonoBehaviour
{
    public GameEvent UpdateNarrativeEvent;
    public Image photo;
    public Color filledColor;
    public Color clearColor;

    public float fadeOutTime;

    public bool canFadeIn;
    public bool canFadeOut;

   

    public void Start()
    {
        filledColor = new Color(photo.color.r, photo.color.g, photo.color.b, 1);
        clearColor = Color.clear;
        canFadeIn = true;
        canFadeOut = false;
    }

    public void FadeInPhoto()
    {

        if (canFadeIn)
        {

            StartCoroutine(FadeInRoutine(clearColor, filledColor));
        }
    }

    public void FadeOutPhoto()
    {
        if (canFadeOut)
        {

            if (photo.color != clearColor)
            {
                StopAllCoroutines();
                StartCoroutine(FadeOutRoutine(photo.color, clearColor));
            }
        }
    }

    private IEnumerator FadeInRoutine(Color startColor, Color endColor)
    {
        canFadeIn = false;
        canFadeOut = true;

        yield return new WaitForSeconds(2f);
        Debug.Log("currentnarrative" + NarrativeController.controller.narrativeID);
        if (NarrativeController.controller.narrativeID != 0)
        {
            for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
            {
                photo.color = Color.Lerp(startColor, endColor, Mathf.Min(1, t / fadeOutTime));

                yield return null;
            }
            NarrativeController.controller.setNextNarrative = true;
        }
        else
        {
            NarrativeController.controller.setNextNarrative = true;
        }

    }

    private IEnumerator FadeOutRoutine(Color startColor, Color endColor)
    {
        canFadeOut = false;
        canFadeIn = true;

        if (NarrativeController.controller.setNextNarrative)
        {
            UpdateNarrativeEvent.Raise();
        }
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            photo.color = Color.Lerp(startColor, endColor, Mathf.Min(1, t / fadeOutTime));

            yield return null;
        }
        


    }

}

