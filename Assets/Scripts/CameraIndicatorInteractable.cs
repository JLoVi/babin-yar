using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraIndicatorInteractable : MonoBehaviour
{

    public Color startColor;
    public Color hoverColor;

    public GameObject cameraToActivate;
    public GameObject lightToActivate;

    public bool inView;

    private void Start()
    {
        inView = false;
        GetComponent<Renderer>().material.color = startColor;
    }
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 5);
        transform.Rotate(Vector3.left * Time.deltaTime * 10);

        if (StageManager.activeCamera == cameraToActivate && inView)
        {
            if (Input.GetMouseButtonDown(0))
            {

                // TeleportPlayer();
            }
        }
    }

    public void OnMouseOver()
    {

    }

    public void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = hoverColor;
    }

    public void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = startColor;
    }

    public void OnMouseDown()
    {
        SwitchCameras();
    }

    public void SwitchCameras()
    {
        StageManager.activeCamera.SetActive(false);
        StageManager.activeLight.SetActive(false);
        StageManager.activeCamera = cameraToActivate;
        StageManager.activeLight = lightToActivate;
        cameraToActivate.SetActive(true);
        lightToActivate.SetActive(true);
        inView = true;
    }

    public void TeleportPlayer()
    {
        cameraToActivate.SetActive(false);
        /*StageManager.player.SetActive(true);
        StageManager.player.transform.position = gameObject.transform.position;
        StageManager.activeCamera = StageManager.player;*/
    }
}
