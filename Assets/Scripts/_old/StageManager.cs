using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    
    public GameObject defaultCamera;
    public GameObject defaultLight;

    public GameObject[] mainPlayerViewObjects;

    public GameObject droneCamera;

    public GameObject player;
    public static GameObject activeCamera;
    public static GameObject activeLight;

    public static StageManager instance;

    // Start is called before the first frame update

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        player = defaultCamera; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDroneView(bool state)
    {
        droneCamera.SetActive(state);
    }


    public void SetMainPlayerView(bool state)
    {
        foreach(GameObject obj in mainPlayerViewObjects)
        {
            obj.SetActive(state);
        }
    }

    public void TeleportPlayer(Vector3 pos, Quaternion rot)
    {
        CharacterController cc = player.GetComponent<CharacterController>();

        cc.enabled = false;
        player.transform.position = pos;
        player.transform.rotation = rot;
        cc.enabled = true;
    }

    
}
