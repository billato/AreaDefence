using UnityEngine;
using System.Collections;

public class PlaceGun : MonoBehaviour {


	public GameObject gunPrefab;
	private GameObject gun;

	private GameObject[] turretSpotsList;

	private GameObject scoreText;
	private GameObject cam;


	//private GameObject mainCamera;



	// Use this for initialization
	void Start () {

		//main = (Camera) Gun.GetComponent (typeof(Camera));
	}





	private bool canPlaceGun() {
		//return gun == null;

		if (gunPrefab == null) {
			return false;
		} else {
			return true;
		}

	}

	void OnMouseUp () {



		Debug.Log (canPlaceGun());
		Debug.Log (this.gunPrefab);

		//2
		if (canPlaceGun ()) {
			//3


			gun = (GameObject) Instantiate(gunPrefab, this.transform.position, Quaternion.identity);
			gun.transform.position = new Vector3 (
				this.transform.position.x,
				this.transform.position.y - 2.8f,
				this.transform.position.z);

			gun.transform.rotation = this.transform.rotation;

			//disable turret spot clicked
			gameObject.SetActive (false);

			//decrease money
			scoreText = GameObject.FindGameObjectWithTag ("Score");
			GameManager gameManagerScript = scoreText.GetComponent<GameManager> ();  //get script GameManager
			gameManagerScript.descreaseScore(gunPrefab.GetComponent<TurretPerimeter>().getMoneyCost());
		



			cam = GameObject.FindGameObjectWithTag ("MainCamera");
			CameraController cameraControllerScript = cam.GetComponent<CameraController> ();  //get script CameraController
			cameraControllerScript.addCamera(gun.GetComponentInChildren<Camera>());






			//4
//			AudioSource audioSource = gameObject.GetComponent<AudioSource>();
//			audioSource.PlayOneShot(audioSource.clip);

			// TODO: Deduct gold


			/*
			GameObject mainCamera = GameObject.Find("Main Camera");
			CameraController camController = (CameraController) mainCamera.GetComponent (typeof(CameraController));

			Camera tmpCamera = (Camera) Gun.GetComponent (typeof(Camera));

			camController.addCamera (tmpCamera);
			*/

		}
	}



	public void setGunToAllPrefabInstances(GameObject gunPrefab){
		PlaceGun placeGunScript; 

		turretSpotsList= GameObject.FindGameObjectsWithTag ("TurretSpot");
		Debug.Log("turretSpotsList:" + turretSpotsList.Length);


		for (int i = 0; i < this.turretSpotsList.Length; i++) {
			placeGunScript = turretSpotsList [i].GetComponent<PlaceGun>();
			placeGunScript.setGun(gunPrefab);

		}

	}


	private void setGun(GameObject gunPrefab){
		this.gunPrefab = gunPrefab;





	}
}
