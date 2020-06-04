using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{

    public ScrollRect m_ScrollRect;

    [SerializeField]
    private int currentPage;
    //  public Transform[] pages;
    public float transitionStep;
    public float canvasEndCorrectedPos;
    private float startPos;
    
    private bool canScroll;
    private int input;

    private int previousValue;
    public AnimationCurve MoveCurve;

    public ShowPhoto photoFader;
  

    void Start()
    {
        canvasEndCorrectedPos = 1 - canvasEndCorrectedPos;
        input = 0;
        canScroll = true;

        currentPage = 0;

        if ( NarrativeController.controller.narrativeID >3)
        {
            ScrollToMiddle();
        }
        if (NarrativeController.controller.narrativeID == 1 || NarrativeController.controller.narrativeID == 2 || NarrativeController.controller.narrativeID == 3)
        {
            ScrollToEnd();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_ScrollRect.verticalNormalizedPosition);
        //  Debug.Log("previousvalue" + previousValue);

        GetCurrentPage();
        startPos = m_ScrollRect.verticalNormalizedPosition;
        var x = Input.GetAxis("Mouse ScrollWheel");

        if (x > 0)
        {
            //  StartCoroutine(SetTOZeroAfterTime());
            input = -2;
            CameraMoveScrollController.forwardScroll = false;
        }

        if (x < 0)
        {
            // StartCoroutine(SetTOZeroAfterTime());

            input = 2;
            CameraMoveScrollController.forwardScroll = true;
        }

     /*   if (x > 0.1f && x < 0.25f)
        {
            // StartCoroutine(SetTOZeroAfterTime());

            input = -1;
            CameraMoveScrollController.forwardScroll = false;
        }

        if (x < -0.1f && x > -0.2f)
        {
            //  StartCoroutine(SetTOZeroAfterTime());

            input = 1;
            CameraMoveScrollController.forwardScroll = true;
        }*/

        if (x == 0)
        {
            input = 0;
        }

        //  Debug.Log(x);
        if (input == 1 && canScroll && previousValue != 1 && previousValue != 2) // forward
        {
            //  input = 0;
            if (NarrativeController.controller.setNextNarrative)
            {
                photoFader.FadeOutPhoto(CameraMoveScrollController.photoToShow);
                return;
            }
            canScroll = false;
           // Debug.Log(input);
            ScrollRectForward();
            previousValue = input;
            StartCoroutine(SetTOZeroAfterTime());
        }
        if (input == -1 && canScroll && previousValue != -1 && previousValue != -2) // backwards
        {

            canScroll = false;
           // Debug.Log(input);
            ScrollRectBack();
            previousValue = input;
            StartCoroutine(SetTOZeroAfterTime());
        }
        //  Debug.Log(x);
        if (input == 2 && canScroll && previousValue != 1 && previousValue != 2) // forward
        {
            if (NarrativeController.controller.setNextNarrative)
            {
                photoFader.FadeOutPhoto(CameraMoveScrollController.photoToShow);
                return;
            }
            canScroll = false;
           // Debug.Log(input);
            ScrollRectForward();
            previousValue = input;
            StartCoroutine(SetTOZeroAfterTime());

        }
        if (input == -2 && canScroll && previousValue != -1 && previousValue != -2) // backwards
        {

            canScroll = false;
          //  Debug.Log(input);
            ScrollRectBack();
            previousValue = input;
            StartCoroutine(SetTOZeroAfterTime());
        }
        if (input == 0)
        {
            StartCoroutine(CheckIfZeroAfterTime());
            //  previousValue = 0;
            //  Debug.Log(input);
        }
    }

    public void ScrollRectForward()
    {

        float scrollTime;
        float targetPos;
        //  Debug.Log("forward");

        

        StartCoroutine(SetCanScroll());


        if (input == 1)
        {
            scrollTime = 0.7f;

            //  targetPos = startPos - 0.35f;
            targetPos = startPos - transitionStep;
        }
        else
        {
            scrollTime = 1f;
            targetPos = 1 - (transitionStep * currentPage);
        }

        IEnumerator co;

        co = ScrollToNormalisedPosition(scrollTime, startPos, targetPos);

        StopCoroutine(co); // stop it.

        if (currentPage == 3)
        {
            targetPos = canvasEndCorrectedPos;
            StartCoroutine(ScrollToNormalisedPosition(scrollTime, startPos, targetPos));
        }
        else
        {
            StartCoroutine(ScrollToNormalisedPosition(scrollTime, startPos, targetPos));
        }
        //  }
        //   else { return; }

    }



    public void ScrollRectBack()
    {


        float scrollTime;
        float targetPos;
      //   Debug.Log("back");
        StartCoroutine(SetCanScroll());
        //  currentPage = currentPage -= 1;
        // GetCurrentPage();

        if (input == -1)
        {
            scrollTime = 0.7f;

            // targetPos = startPos + 0.35f;
            targetPos = startPos + transitionStep;
        }
        else
        {
            scrollTime = 1f;
            targetPos = startPos + transitionStep;
        }

        IEnumerator co;

        // co = ScrollToNormalisedPosition(1f, startPos, startPos + transitionStep); // create an IEnumerator object
        co = ScrollToNormalisedPosition(scrollTime, startPos, targetPos);
        //StartCoroutine(co); // start the coroutine
        StopCoroutine(co); // stop it.


        if (currentPage == 1)
        {
            StartCoroutine(ScrollToNormalisedPosition(scrollTime, startPos, 1));
        }

        if (currentPage == 3)
        {
            StartCoroutine(ScrollToNormalisedPosition(scrollTime, startPos, startPos + transitionStep));
        }
        else
        {
            //StartCoroutine(ScrollToNormalisedPosition(scrollTime, startPos, startPos + transitionStep));
            StartCoroutine(ScrollToNormalisedPosition(scrollTime, startPos, targetPos));
        }
        //   }
        //   else { return; }

    }

    public IEnumerator ScrollToNormalisedPosition(float scrollTime, float currentPos, float targetPos)
    {

        for (float t = 0.01f; t < scrollTime; t += Time.deltaTime)
        {
            m_ScrollRect.verticalNormalizedPosition = Mathf.Lerp(currentPos, targetPos, MoveCurve.Evaluate(Mathf.Min(1, t / scrollTime)));
            yield return null;

        }

       
    }

    public IEnumerator SetCanScroll()
    {

        yield return new WaitForSeconds(1.2f);
        input = 0;
        canScroll = true;
        // previousValue = 0;

    }

    public IEnumerator CheckIfZeroAfterTime()
    {
        yield return new WaitForSeconds(1.5f);
        if (input == 0)
        {
            previousValue = 0;

            // canScroll = true;
            //  Debug.Log(input);
        }

    }

    public IEnumerator SetTOZeroAfterTime()
    {
        yield return new WaitForSeconds(1.2f);
        // input = 0;
        previousValue = 0;

    }

    public void GetCurrentPage()
    {
        float verticalPos = m_ScrollRect.verticalNormalizedPosition;


        if (verticalPos > 0.25f && verticalPos <= 0.5f)
        {
            currentPage = 3;
        }

        if (verticalPos > 0.5f && verticalPos <= 0.75f)
        {
            currentPage = 2;
        }

        if (verticalPos > 0.75f && verticalPos <= 1f)
        {
            currentPage = 1;
        }
        // Debug.Log(currentPage);
    }

    public void ScrollToMiddle()
    {
        StartCoroutine(ScrollToNormalisedPosition(2f, 0.99f, 0.6f));
    }

    public void ScrollToEnd()
    {
        StartCoroutine(ScrollToNormalisedPosition(2f, 0.99f, canvasEndCorrectedPos));
    }

    public void ScrollToBlank()
    {
        StartCoroutine(ScrollToNormalisedPosition(2f, 0.99f, 0.1f));
    }
}
