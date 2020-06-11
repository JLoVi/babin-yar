using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTerrain : MonoBehaviour

{

    public GameObject[] terrainModules;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        foreach (GameObject go in terrainModules)
        {
            go.SetActive(false);
        } 
        this.transform.parent.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        foreach (GameObject go in terrainModules)
        {
            go.SetActive(true);
            
        }
    }
}
