﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveScrollController : MonoBehaviour
{
    //  public GameEvent onUpdateNarrative;
    public ShowPhoto photoFader;
    private Camera mainCam;
    private bool adjustingCam;

    public float normalizedT;
    public static ScrollRect scrollcanvas;
    //  public static Image photoToShow;

    public static Transform target1;
    public static Transform target2;
    // public Transform startCam;
    public static Transform endCam;

    public static bool forwardScroll;

    public float movementTime = 1;
    public float rotationSpeed = 0.1f;
    public float camLerpSpeed = 1f;


    Vector3 refPos;
    Vector2 refCam;

    public bool canMoveCam;
    public bool scrollToEnd;

    public static CameraMoveScrollController controller;

    void Awake()
    {
        controller = this;
        mainCam = this.GetComponent<Camera>();

    }
    // Start is called before the first frame update
    void Start()
    {
        canMoveCam = true;
        scrollToEnd = false;
        adjustingCam = false;
    }

    // Update is called once per frame
    void Update()
    {
        normalizedT = 1 - scrollcanvas.verticalNormalizedPosition;

        if (normalizedT < 0.2 && !scrollToEnd)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target2.position, ref refPos, movementTime);
            //Interpolate Rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target2.rotation, rotationSpeed * Time.deltaTime);

        }


        if (normalizedT > 0.6)
        {

            MoveToEndCamera();
            if (endCam.GetComponent<Camera>() != null) { 
                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, endCam.GetComponent<Camera>().fieldOfView, rotationSpeed * Time.deltaTime);
               // mainCam.lensShift = Vector2.Lerp(mainCam.fieldOfView, endCam.GetComponent<Camera>().fieldOfView.x, rotationSpeed * Time.deltaTime);
            }
          
        }



        else
        {
            NarrativeController.controller.setNextNarrative = false;
            photoFader.FadeOutPhoto();

            // transform.rotation = Quaternion.Slerp(transform.rotation, target2.rotation, rotationSpeed * Time.deltaTime);
            if (GetComponent<CameraPanController>() != null)
            {
                GetComponent<CameraPanController>().enabled = true;
            }

            if (GetComponent<CameraOrbitController>() != null)
            {
                GetComponent<CameraOrbitController>().enabled = true;
            }
        }
    }

    public void MoveToEndCamera()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, endCam.rotation, rotationSpeed * Time.deltaTime);

        transform.position = Vector3.SmoothDamp(transform.position, endCam.position, ref refPos, movementTime);



        if (GetComponent<CameraPanController>() != null)
        {
            GetComponent<CameraPanController>().enabled = false;
        }

        if (GetComponent<CameraOrbitController>() != null)
        {
            GetComponent<CameraOrbitController>().enabled = false;
        }

        if (normalizedT > 0.65)
        {
            NarrativeController.controller.SetCurrentNarrativePhoto();
            photoFader.FadeInPhoto();
        }
    }

    public IEnumerator AdjustPhysicalCamera()
    {
        adjustingCam = true;
        if (endCam.GetComponent<Camera>() != null)
        {
            for (float t = 0.01f; t < camLerpSpeed; t += Time.deltaTime)
            {

                mainCam.fieldOfView = Mathf.Lerp(mainCam.fieldOfView, endCam.GetComponent<Camera>().fieldOfView, Mathf.Min(1, t / camLerpSpeed));
                mainCam.lensShift = Vector2.Lerp(mainCam.lensShift, endCam.GetComponent<Camera>().lensShift, Mathf.Min(1, t / camLerpSpeed));

                yield return null;
            }
            adjustingCam = false;

        }
    }

    public void SetMoveValues()
    {
        if (canMoveCam)
        {
            normalizedT = 1 - scrollcanvas.verticalNormalizedPosition;

            if (forwardScroll)
            {
                transform.position = Vector3.Lerp(transform.position, target1.position, normalizedT / 100);
            }
            if (!forwardScroll)
            {
                transform.position = Vector3.Lerp(transform.position, target2.position, normalizedT / 100);
            }
        }
        else
        {
            return;
        }

    }

}
