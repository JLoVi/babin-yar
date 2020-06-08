using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{

    public int narrativeID;

    public NarrativeItem[] narrativeItems;

    public NarrativeDataObject currentNarrative;

    public Image[] photos;
    public Image currentNarrativePhoto;

    public static NarrativeController controller;

    public GameObject[] scrollPanels;

    public bool setNextNarrative;

    void Awake()
    {
        controller = this;

        narrativeID = 0;

        foreach (GameObject go in scrollPanels)
        {
            go.SetActive(false);
        }
        SetCurrentNarrativePhoto();
        SwitchNarrative();
    }

    void Start()
    {
        foreach (NarrativeItem marker in narrativeItems)
        {
            marker.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNextNarrative()
    {
        if (narrativeID < narrativeItems.Length - 1)
        {
            foreach (GameObject go in scrollPanels)
            {
                go.SetActive(false);
            }
            setNextNarrative = false;
            narrativeID++;
            SwitchNarrative();
        }
    }

    private void SwitchNarrative()
    {
        GetCurrentNarrativeData();
        SetCurrentNarrativeScrollPanel();
        //  SetCurrentNarrativePhoto();
        currentNarrative.SetAnimationTargets();
    }

    private void GetCurrentNarrativeData()
    {
        currentNarrative = narrativeItems[narrativeID].GetComponent<NarrativeDataObject>();
    }

    private void SetCurrentNarrativeScrollPanel()
    {

        narrativeItems[narrativeID].scrollPanel.SetActive(true);
        narrativeItems[narrativeID].SetScrollRects();
    }

    public void SetCurrentNarrativePhoto()
    {
        currentNarrativePhoto = narrativeItems[narrativeID].photo;
    }

    public void ActiivateMarkers(bool state, float delay)
    {
        StartCoroutine(ActivateMarkersRoutiine(state, delay));
    }

    public IEnumerator ActivateMarkersRoutiine(bool state, float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (NarrativeItem marker in narrativeItems)
        {
            yield return new WaitForSeconds(0.1f);
            marker.gameObject.SetActive(state);
        }

    }
}

