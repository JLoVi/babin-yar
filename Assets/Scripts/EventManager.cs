using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void OnFadeOutPhoto()
    {
        OnFPSActivated();

    }

    public void OnDroneActivated()
    {
        StageManager.activeCamera.SetActive(false);
        StageManager.activeCamera = StageManager.instance.droneCamera;
        StageManager.instance.SetDroneView(true);
    }

    public void OnFPSActivated()
    {
        StageManager.activeCamera.SetActive(false);
        StageManager.activeCamera = StageManager.instance.defaultCamera;
        StageManager.instance.SetMainPlayerView(true);
    }
}
