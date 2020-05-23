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

    void Start()
    {
        canScroll = true;

        currentPage = 0;
        //transitionStep = (float)1 / pages.Length;
        transitionStep = 0.22f;
    }

    // Update is called once per frame
    void Update()
    {
        startPos = m_ScrollRect.verticalNormalizedPosition;
         var x = Input.GetAxis("Mouse ScrollWheel");
        // var input = if (x > 0) { }
        int input = 0;

        if (x > 0.2 &&  x < 0.5f)
        {
            input = 1;
        }
        if (x < - 0.2 && x > -0.5f)
        {
            input = -1;
        }
        if (x == 0)
        {
            input = 0;
        }


        //  Debug.Log(x);
        if (input == 1 && canScroll) // forward
        {
           
            
            ScrollRectForward();

        }
        if (input == -1 && canScroll) // backwards
        {
           
           
            ScrollRectBack();
        }
    }

    public void ScrollRectForward()
    {
        if (currentPage < 5)
        {
          //  Debug.Log("forward");
            StartCoroutine(SetCanScroll());
            currentPage = currentPage += 1;

            float targetPos = 1 - (transitionStep * currentPage);

            IEnumerator co;

            co = ScrollToNormalisedPosition(1f, startPos, targetPos); // create an IEnumerator object
          //  StartCoroutine(co); // start the coroutine
            StopCoroutine(co); // stop it.

            

            if (currentPage == 5)
            {
                StartCoroutine(ScrollToNormalisedPosition(1f, startPos, 0));
            }
            else
            {
                StartCoroutine(ScrollToNormalisedPosition(1f, startPos, targetPos));
            }
        }
        else { return; }

    }

    public void ScrollRectBack()
    {
        if (currentPage > 0)
        {
           // Debug.Log("back");
            StartCoroutine(SetCanScroll());
            currentPage = currentPage -= 1;

            IEnumerator co;

            co = ScrollToNormalisedPosition(1f, startPos, startPos + transitionStep); // create an IEnumerator object
            //StartCoroutine(co); // start the coroutine
            StopCoroutine(co); // stop it.

            
            if (currentPage == 0)
            {
                StartCoroutine(ScrollToNormalisedPosition(0.8f, startPos, 1));
            }
            else
            {
                StartCoroutine(ScrollToNormalisedPosition(0.8f, startPos, startPos + transitionStep));
            }
        }
        else { return; }

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
        canScroll = false;
        yield return new WaitForSeconds(0.6f);
        canScroll = true;

    }
}
