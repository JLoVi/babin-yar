using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraMoveScrollController : MonoBehaviour
{

    public float normalizedT;
    public ScrollRect scrollcanvas;
    public Transform target1;
    public Transform target2;
   // public Transform startCam;
    public Transform endCam;

    public static bool forwardScroll;

    public float movementTime = 1;
    public float rotationSpeed = 0.1f;

    Vector3 refPos;
    Vector3 refRot;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (normalizedT < 0.1)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target2.position, ref refPos, movementTime);
            //Interpolate Rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target2.rotation, rotationSpeed * Time.deltaTime);

        }
       

        if (normalizedT > 0.9)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, endCam.rotation, rotationSpeed * Time.deltaTime);

            transform.position = Vector3.SmoothDamp(transform.position, endCam.position, ref refPos, movementTime);
            //Interpolate Rotation

            if (GetComponent<CameraPanController>() != null)
            {
                GetComponent<CameraPanController>().enabled = false;
            }

            if (GetComponent<CameraOrbitController>() != null)
            {
                GetComponent<CameraOrbitController>().enabled = false;
            }
        }
        else
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, target2.rotation, rotationSpeed * Time.deltaTime);
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

    public void SetMoveValues()
    {
        normalizedT = 1 - scrollcanvas.verticalNormalizedPosition;
//        Debug.Log(forwardScroll);
        if (forwardScroll)
        {
            transform.position = Vector3.Lerp(transform.position, target1.position, normalizedT / 100);
        }
        if (!forwardScroll)
        {
            transform.position = Vector3.Lerp(transform.position, target2.position, normalizedT / 100);
        }

    }

}
