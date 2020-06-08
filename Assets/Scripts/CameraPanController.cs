using UnityEngine;

public class CameraPanController : MonoBehaviour
{
  

    public float panSpeed = 30f;
    public float panBorderThickness;

   
 
    private void Start()
    {
        
    }

    void Update()
    {
       

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

            transform.Translate(Vector3.right * panSpeed * Time.deltaTime);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {

            transform.Translate(Vector3.left * panSpeed * Time.deltaTime);
        }

       
        Vector3 pos = transform.position;

        transform.position = pos;
    }


}
