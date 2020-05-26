using System;
using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{

    public Transform target;
    public float speed;
    private bool isLeft = true;

    void Start()
    {
      
    }

    void Update()
    {
        transform.LookAt(target);

        if (isLeft)
            transform.position += transform.right * speed * Time.deltaTime;
        else
            transform.position -= transform.right * speed * Time.deltaTime;

        if (Input.GetMouseButton(0))
        {
            var fT = Input.GetAxis("Mouse X");
            if (fT > 0.01)
                isLeft = false;
            else if (fT < -0.01)
                isLeft = true;

            transform.LookAt(target);
            transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * speed);
        }
    }
}