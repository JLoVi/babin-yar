using UnityEngine;

public class CameraOrbitController : MonoBehaviour
{

    protected Transform _XForm_Camera;
    protected Transform _XForm_Parent;

    protected Vector3 _WorldRotation;
    protected float _CameraDistance = 10f;

    public float MouseSensitivity = 4f;
    public float ScrollSensitivity = 2f;
    public float OrbitDampening = 10f;
    public float ScrollDampening = 6f;

    // Start is called before the first frame update
    void Start()
    {
        this._XForm_Camera = this.transform;
        this._XForm_Parent = this.transform.parent;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       

        if (Input.GetMouseButton(0))
        {
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                _WorldRotation.x += Input.GetAxis("Mouse X") * MouseSensitivity;
                _WorldRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitivity;

                _WorldRotation.y = Mathf.Clamp(_WorldRotation.y, -20f, 60f);
                _WorldRotation.x = Mathf.Clamp(_WorldRotation.x, -60, 30);
            }
        }

        Quaternion QT = Quaternion.Euler(_WorldRotation.y, _WorldRotation.x, 0);
        this._XForm_Parent.rotation = Quaternion.Lerp(this._XForm_Parent.rotation, QT, Time.deltaTime * OrbitDampening);
     
    }
}
