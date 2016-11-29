using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public Camera[] cameras;
	private int currentCameraIndex;
	private int previousCameraIndex;

	// Use this for initialization
	void Start () {
		previousCameraIndex = 0;
		currentCameraIndex = 0;

		//Turn all cameras off, except the first default one
		for (int i = 1; i < cameras.Length; i++) {
			if (cameras [i] != null) {
				((AudioListener)cameras [i].GetComponent (typeof(AudioListener))).enabled = false;
				cameras [i].gameObject.SetActive (false);
			}
		}

		//If any cameras were added to the controller, enable the first one
		if (cameras.Length>0)
		{
			cameras [0].gameObject.SetActive (true);
			Debug.Log ("Camera with name: " + cameras [0].GetComponent<Camera>().name + ", is now enabled");
		}
	}

	// Update is called once per frame
	void Update () {
		//If the c button is pressed, switch to the next camera
		//Set the camera at the current index to inactive, and set the next one in the array to active
		//When we reach the end of the camera array, move back to the beginning or the array.
		if (Input.GetKeyDown(KeyCode.C))
		{
			previousCameraIndex = currentCameraIndex;
			currentCameraIndex ++;


			if (currentCameraIndex < cameras.Length) {
			


				//disable audio on previous camera
				//((AudioListener) cameras [currentCameraIndex - 1].GetComponent (typeof(AudioListener))).enabled = false;

				//disable current camera except default one
				if (currentCameraIndex - 1 > 0) {
					cameras [currentCameraIndex - 1].gameObject.SetActive (false);
					((AudioListener)cameras [currentCameraIndex - 1].GetComponent (typeof(AudioListener))).enabled = false;

				} 


				//enable audio on current camera
				((AudioListener) cameras [currentCameraIndex].GetComponent (typeof(AudioListener))).enabled = true;

				//enable current camera
				cameras[currentCameraIndex].gameObject.SetActive(true);

				Debug.Log("totalCameras:" + cameras.Length);

				Debug.Log ("[" + previousCameraIndex + "," + currentCameraIndex + "," + cameras.Length + "] " +
					"C button has been pressed. Switching to the next camera (" + cameras [currentCameraIndex].GetComponent<Camera>().name + ").");
			}
			else
			{
				//disable audio on previous camera
				((AudioListener) cameras [previousCameraIndex].GetComponent (typeof(AudioListener))).enabled = false;
				//disable previous camera
				cameras[previousCameraIndex].gameObject.SetActive(false);

				//previousCameraIndex = 0;
				currentCameraIndex = 0;

				//enable audio on current camera
				((AudioListener) cameras [currentCameraIndex].GetComponent (typeof(AudioListener))).enabled = true;

				//enable current camera
				cameras[currentCameraIndex].gameObject.SetActive(true);



				Debug.LogError ("[" + previousCameraIndex + "," + currentCameraIndex + "," + cameras.Length + "] " +
					"C button has been pressed. Switching to the next camera (" + cameras [currentCameraIndex].GetComponent<Camera>().name + ").");

			}
		}
	}


	public void addCamera(Camera camera){
		for (int i=1; i<cameras.Length; i++) 
		{
			if (cameras [i] == null) {

				camera.enabled = true;
				cameras [i] = camera;
				break;
			}
		}
	}



}