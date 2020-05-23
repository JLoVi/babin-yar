using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera cameraDrone;
    public static bool doMovement = true;

    public float panSpeed = 30f;
    public float panBorderThickness;

    public float scrollSpeed = 5f;
    public float minZoom;
    public float maxZoom;

    public float camSize;

    private void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {

            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {

            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {

            transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {

            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");

        camSize += scroll * scrollSpeed;

        camSize = Mathf.Clamp(camSize, minZoom, maxZoom);


        Vector3 pos = transform.position;

        transform.position = pos;
    }


}
