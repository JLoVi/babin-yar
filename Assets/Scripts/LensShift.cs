using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LensShift : MonoBehaviour
{

    public Vector2 lensShift;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Camera>().lensShift = lensShift;
    }
}
