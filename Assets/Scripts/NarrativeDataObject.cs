using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeDataObject : MonoBehaviour
{
    public GameObject endposition;
    public GameObject target1;
    public GameObject target2;

    public GameObject hoverIcon;

    public Camera mainCam;

    private void Start()
    {
        SetAnimationTargets();
    }


    void LateUpdate()
    {
        Vector3 screenPos = mainCam.WorldToScreenPoint(endposition.transform.position);
        this.transform.position = new Vector3(screenPos.x, screenPos.y, this.transform.position.z);
    }

    public void DisplayTargetDescription(bool state)
    {
        hoverIcon.SetActive(state);
    }

    public void SetAnimationTargets()
    {
        CameraMoveScrollController.target1 = target1.transform;
        CameraMoveScrollController.target2 = target2.transform;
        CameraMoveScrollController.endCam = endposition.transform;
    }

}
