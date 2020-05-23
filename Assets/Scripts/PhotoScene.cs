using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoScene : MonoBehaviour
{

    public GameObject[] photoSceneObjects;
    

    public void SetSceneObjects (bool state)
    {
        foreach(GameObject obj in photoSceneObjects)
        {
            obj.SetActive(state);
        }
    }
}
