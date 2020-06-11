using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPhoto : MonoBehaviour
{
    //  public Image photo;
    public Color filledColor;
    public Color clearColor;

    public float fadeOutTime;

    public bool canFadeIn;
    public bool canFadeOut;



    public void Start()
    {

        filledColor = new Color(NarrativeController.controller.currentNarrativePhoto.color.r,
             NarrativeController.controller.currentNarrativePhoto.color.g,
             NarrativeController.controller.currentNarrativePhoto.color.b, 0.8f);
        clearColor = Color.clear;

        canFadeIn = true;
        canFadeOut = false;
    }

    public void FadeInPhoto()
    {
        if (canFadeIn)
        {
            NarrativeController.controller.SetCurrentNarrativePhoto();

            if (NarrativeController.controller.narrativeID <= NarrativeController.controller.narrativeItems.Length - 1)
            {
                if (NarrativeController.controller.narrativeID == NarrativeController.controller.narrativeItems.Length - 1 && NarrativeController.controller.setNextNarrative)
                {
                    NarrativeController.controller.restartButton.SetActive(true);
                    return;
                }
                StartCoroutine(FadeInRoutine(clearColor, filledColor, NarrativeController.controller.currentNarrativePhoto));
            }

        }
    }

    public void FadeOutPhoto()
    {

        if (canFadeOut)
        {


            NarrativeController.controller.SetCurrentNarrativePhoto();

            StopAllCoroutines();
            foreach (Image photo in NarrativeController.controller.photos)
            {
                StartCoroutine(FadeOutRoutine(photo.color, clearColor, photo));
            }

        }
    }

    private IEnumerator FadeInRoutine(Color startColor, Color endColor, Image photo)
    {
        canFadeIn = false;
        canFadeOut = true;

        yield return new WaitForSeconds(2f);
        //        Debug.Log("currentnarrative" + NarrativeController.controller.narrativeID);
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

    private IEnumerator FadeOutRoutine(Color startColor, Color endColor, Image photo)
    {
        canFadeOut = false;
        canFadeIn = true;

        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            photo.color = Color.Lerp(startColor, endColor, Mathf.Min(1, t / fadeOutTime));

            yield return null;
        }
        photo.color = clearColor;
        if (NarrativeController.controller.narrativeID == NarrativeController.controller.narrativeItems.Length - 1 && NarrativeController.controller.setNextNarrative)
        {
            NarrativeController.controller.restartButton.SetActive(true);
        }

    }

    public IEnumerator FadeOutPhotoSimple(Color startColor, Color endColor, Image photo)
    {
        for (float t = 0.01f; t < 0.3; t += Time.deltaTime)
        {
            photo.color = Color.Lerp(startColor, endColor, Mathf.Min(1, t / fadeOutTime));

            yield return null;
        }
    }
}

