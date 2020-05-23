using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CameraAnimScrollController : MonoBehaviour
{

    public Animator animator;
    public float normalizedT;
    public ScrollRect scrollcanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAnimValues()
    {
        normalizedT = 1 - scrollcanvas.verticalNormalizedPosition;
         animator.Play(0, -1, normalizedT);
    }
}
