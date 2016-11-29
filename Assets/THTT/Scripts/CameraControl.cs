//simple RTS camera
// BY David Harrison

using UnityEngine;
using System.Collections;
//controls the camera movement
public class CameraControl : MonoBehaviour {
    private float scrollArea = 10;
    private Transform myTransform;
    public float ScrollSpeed = 15;
    public float MoveSpeed = 15;
    public float DragSpeed = 15;
   

    public bool PosMov = false;

	// Use this for initialization
	void Start () {
        //scrollArea = Screen.width;
        myTransform = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        var mPosX = Input.mousePosition.x;
        var mPosY = Input.mousePosition.y;

        // Do camera movement by mouse position
        if (PosMov)
        {
            if (mPosX < scrollArea) { myTransform.Translate(Vector3.right * -MoveSpeed * Time.deltaTime); }
            if (mPosX >= Screen.width - scrollArea) { myTransform.Translate(Vector3.right * MoveSpeed * Time.deltaTime); }
            if (mPosY < scrollArea) { myTransform.Translate(0, 0, -MoveSpeed * Time.deltaTime, Space.World); }
            if (mPosY >= Screen.height - scrollArea) { myTransform.Translate(0, 0, MoveSpeed * Time.deltaTime, Space.World); }
        }

        // Do camera movement by keyboard
        myTransform.Translate(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime,
                                      0,Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime, Space.World);

        // Do camera movement by holding down option                 or middle mouse button and then moving mouse
        if ((Input.GetKey("left alt") || Input.GetKey("right alt")) || Input.GetMouseButton(2))
        {
            myTransform.Translate(-new Vector3(Input.GetAxis("Mouse X") * DragSpeed, Input.GetAxis("Mouse Y") * DragSpeed, 0));
        }
	

        //Scroll with mouse wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0)
        {
            float test = myTransform.transform.position.y + (-Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed);
            if (test > 0 && test < 600) 
            myTransform.Translate(0, -Input.GetAxis("Mouse ScrollWheel") * ScrollSpeed, 0, Space.World);
        }
       

        
	}
}
