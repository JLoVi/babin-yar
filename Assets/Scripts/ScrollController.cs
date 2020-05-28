using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour
{

    public ScrollRect m_ScrollRect;
    public int currentPage;
    public Transform[] pages;
    public float transitionStep;
    public float startPos;

    public bool canScroll;
    public int input;

    public int previousValue;



    void Start()
    {
        input = 0;
        currentPage = 1;

        canScroll = true;

        currentPage = 0;
        //transitionStep = (float)1 / pages.Length;
        transitionStep = 0.22f;


    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(m_ScrollRect.verticalNormalizedPosition);
      //  Debug.Log("previousvalue" + previousValue);
        startPos = m_ScrollRect.verticalNormalizedPosition;
        var x = Input.GetAxis("Mouse ScrollWheel");

        if (x > 0.25f)
        {
          //  StartCoroutine(SetTOZeroAfterTime());
            input = -2;
            CameraMoveScrollController.forwardScroll = false;
        }

        if (x < -0.25)
        {
           // StartCoroutine(SetTOZeroAfterTime());

            input = 2;
            CameraMoveScrollController.forwardScroll = true;
        }

        if (x > 0.1f && x < 0.25f)
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
        }

        if (x == 0)
        {
            input = 0;
        }

        //  Debug.Log(x);
        if (input == 1 && canScroll && previousValue != 1 && previousValue != 2) // forward
        {
            //  input = 0;
           
            canScroll = false;
            Debug.Log(input);
            ScrollRectForward();
            previousValue = input;
            StartCoroutine(SetTOZeroAfterTime());
        }
        if (input == -1 && canScroll && previousValue != -1 && previousValue != -2) // backwards
        {
            // input = 0;
           
            canScroll = false;
            Debug.Log(input);
            ScrollRectBack();
            previousValue = input;
            StartCoroutine(SetTOZeroAfterTime());
        }
        //  Debug.Log(x);
        if (input == 2 && canScroll && previousValue != 1 && previousValue != 2) // forward
        {
            //  input = 0;
           
            canScroll = false;
            Debug.Log(input);
            ScrollRectForward();
            previousValue = input;
            StartCoroutine(SetTOZeroAfterTime());

        }
        if (input == -2 && canScroll && previousValue != -1 && previousValue != -2) // backwards
        {
            //  input = 0;
            
            canScroll = false;
            Debug.Log(input);
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
        // if (currentPage < 5)
        // {
        

        float scrollTime;
        float targetPos;
        //  Debug.Log("forward");
        StartCoroutine(SetCanScroll());

        //  currentPage = currentPage += 1;
        GetCurrentPage();

        if (input == 1)
        {
            scrollTime = 0.5f;
            targetPos = startPos - 0.07f;
            // targetPos = startPos + transitionStep / 2;
        }
        else
        {
            scrollTime = 0.8f;
            targetPos = 1 - (transitionStep * currentPage);
        }
       
        IEnumerator co;

        co = ScrollToNormalisedPosition(scrollTime, startPos, targetPos);

        StopCoroutine(co); // stop it.

        if (currentPage == 5)
        {
            targetPos = 0;
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
        //  if (currentPage > 0)
        //  {
        

        float scrollTime;
        float targetPos;
        // Debug.Log("back");
        StartCoroutine(SetCanScroll());
        //  currentPage = currentPage -= 1;
        GetCurrentPage();

        if (input == -1)
        {
            scrollTime = 0.5f;
            targetPos = startPos + 0.07f;
            // targetPos = startPos - transitionStep / 2;
        }
        else
        {
            scrollTime = 0.8f;
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

        if (currentPage == 5)
        {
            StartCoroutine(ScrollToNormalisedPosition(scrollTime, startPos, startPos+transitionStep));
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
            m_ScrollRect.verticalNormalizedPosition = Mathf.Lerp(currentPos, targetPos, Mathf.Min(1, t / scrollTime));
            yield return null;

        }

    }

    public IEnumerator SetCanScroll()
    {

        yield return new WaitForSeconds(0.4f);
        input = 0;
        canScroll = true;
        // previousValue = 0;

    }

    public IEnumerator CheckIfZeroAfterTime()
    {
        yield return new WaitForSeconds(0.2f);
        if (input == 0)
        {
            previousValue = 0;

            // canScroll = true;
            //  Debug.Log(input);
        }

    }

    public IEnumerator SetTOZeroAfterTime()
    {
        yield return new WaitForSeconds(0.6f);
       // input = 0;
        previousValue = 0;

    }

    public void GetCurrentPage()
    {
        float verticalPos = m_ScrollRect.verticalNormalizedPosition;


        if (verticalPos >= 0 && verticalPos <= 0.2f)
        {
            currentPage = 5;
        }

        if (verticalPos > 0.2 && verticalPos <= 0.4f)
        {
            currentPage = 4;
        }

        if (verticalPos > 0.4f && verticalPos <= 0.6f)
        {
            currentPage = 3;
        }

        if (verticalPos > 0.6 && verticalPos <= 0.8f)
        {
            currentPage = 2;
        }

        if (verticalPos > 0.8 && verticalPos <= 1f)
        {
            currentPage = 1;
        }
        // Debug.Log(currentPage);
    }
}
