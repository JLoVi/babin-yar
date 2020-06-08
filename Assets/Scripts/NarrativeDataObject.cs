using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeDataObject : MonoBehaviour
{
    public GameObject endposition;
    public GameObject target1;
    public GameObject target2;

    public GameObject[] hoverIcon;

    public Camera mainCam;

    public bool offset;


    private void Start()
    {
        //  SetAnimationTargets();
        foreach (GameObject icon in hoverIcon)
        {
            icon.SetActive(false);
        }
    }


    void LateUpdate()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(endposition.transform.position);
        if (offset)
        {
            this.transform.position = new Vector3(screenPos.x - 20, screenPos.y + 40, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(screenPos.x, screenPos.y, this.transform.position.z);
        }
    }

    public void DisplayTargetDescription(bool state)
    {

        foreach (GameObject icon in hoverIcon)
        {
            icon.SetActive(state);
        }

    }

    public void SetAnimationTargets()
    {
        CameraMoveScrollController.target1 = target1.transform;
        CameraMoveScrollController.target2 = target2.transform;
        CameraMoveScrollController.endCam = endposition.transform;
    }

}
