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

        filledColor = new Color(CameraMoveScrollController.photoToShow.color.r,
            CameraMoveScrollController.photoToShow.color.g,
            CameraMoveScrollController.photoToShow.color.b, 0.8f);
        clearColor = Color.clear;

        canFadeIn = true;
        canFadeOut = false;
    }

    public void FadeInPhoto()
    {

        if (canFadeIn)
        {

            StartCoroutine(FadeInRoutine(clearColor, filledColor, CameraMoveScrollController.photoToShow));
        }
    }

    public void FadeOutPhoto()
    {
       

        if (canFadeOut)
        {

            if (CameraMoveScrollController.photoToShow.color != clearColor)
            {
                StopAllCoroutines();
                StartCoroutine(FadeOutRoutine(CameraMoveScrollController.photoToShow.color, clearColor, CameraMoveScrollController.photoToShow));
            }
        }
    }

    private IEnumerator FadeInRoutine(Color startColor, Color endColor, Image photo)
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

    private IEnumerator FadeOutRoutine(Color startColor, Color endColor, Image photo)
    {
        canFadeOut = false;
        canFadeIn = true;

        
        for (float t = 0.01f; t < fadeOutTime; t += Time.deltaTime)
        {
            photo.color = Color.Lerp(startColor, endColor, Mathf.Min(1, t / fadeOutTime));

            yield return null;
        }

        NarrativeController.controller.SetCurrentNarrativePhoto();


    }

}

