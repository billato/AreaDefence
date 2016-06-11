using UnityEngine;
using System.Collections;

public class TurretPerimeter : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	

	}

	void OnTriggerEnter(Collider other){


		if (other.gameObject.CompareTag("Enemy")){
			other.gameObject.SetActive (false);



			//GameManager.GetInstance ().increaseScore();

			//play sound each time token is picked up
			//AudioSource.PlayClipAtPoint (bleepAudio[0],transform.position);

		}
	}

}
