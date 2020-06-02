using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMarkers : MonoBehaviour
{
    public GameObject[] cameras;
    public Camera maincam;

    public GameObject markerPrefab;
    public GameObject markerCanvas;
    public GameObject marker;
    public List<GameObject> markers;

    private void Start()
    {
        foreach (GameObject cam in cameras)
        {
            marker = Instantiate(markerPrefab, markerCanvas.transform);
            markers.Add(marker);
        }
    }


    void LateUpdate()
    {
        
        // Debug.Log("target is " + screenPos.x + " pixels from the left");
        for (int i = 0; i < cameras.Length; i++)
        {
            Vector3 screenPos = maincam.WorldToScreenPoint(cameras[i].transform.position);
            markers[i].transform.position = new Vector3(screenPos.x, screenPos.y, markers[i].transform.position.z);
        }

    }
}
