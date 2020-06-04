using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeController : MonoBehaviour
{

    public int narrativeID;

    public NarrativeItem[] narrativeItems;

    public NarrativeDataObject currentNarrative;

    public static NarrativeController controller;

    public GameObject[] scrollPanels;

    public bool setNextNarrative;

    void Awake()
    {
        controller = this;

        narrativeID = 0;

        SwitchNarrative();
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNextNarrative()
    {
        if (narrativeID < narrativeItems.Length-1)
        {
            setNextNarrative = false;
            narrativeID++;
            SwitchNarrative();
        }
    }

    private void SwitchNarrative()
    {
        GetCurrentNarrativeData();
        SetCurrentNarrativeScrollPanel();
        SetCurrentNarrativePhoto();
        currentNarrative.SetAnimationTargets();
    }

    private void GetCurrentNarrativeData()
    {
        currentNarrative = narrativeItems[narrativeID].GetComponent<NarrativeDataObject>();
    }

    private void SetCurrentNarrativeScrollPanel()
    {
        foreach (GameObject go in scrollPanels)
        {
            go.SetActive(false);
        }
        narrativeItems[narrativeID].scrollPanel.SetActive(true);
        narrativeItems[narrativeID].SetScrollRects();
    }

    private void SetCurrentNarrativePhoto()
    {
        narrativeItems[narrativeID].SetPhotoToShow();
    }

}