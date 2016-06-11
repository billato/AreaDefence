using UnityEngine;
using System.Collections;

public class TurretPerimeter : MonoBehaviour {

	public AudioClip[] sounds;

	private bool isPlaying = false;
	private bool isFired = false;

	private float firingTime = 0.5f;

	public Rigidbody missile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	

	}

	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.CompareTag("target")){
			//other.gameObject.SetActive (false);
			//GameManager.GetInstance ().increaseScore();
					
			StartCoroutine(fire());

		}
	}


	void OnTriggerStay(Collider other){

		if (other.gameObject.CompareTag ("target")) {
			
			StartCoroutine(fire());

			//turret head follow the enemy
			this.transform.GetChild (2).LookAt  (other.transform.position); //Turret head looking at enemy 
			//turret base rotate to the enemy
			this.transform.GetChild (0).eulerAngles = new Vector3(0,this.transform.GetChild (2).eulerAngles.y,0);


		}
	}


	IEnumerator fire(){
		if (isPlaying==false || isFired ==false) {
			
			isPlaying = true;
			isFired = true;


			//play sound each time token is picked up
			AudioSource.PlayClipAtPoint (sounds [0], transform.position);
			//Instantiate (missile, transform.position, transform.rotation);
			Instantiate (missile
				, this.transform.GetChild (2).GetChild (0).position //+ new Vector3(0,15,-10)
				,this.transform.GetChild (2).rotation);

			yield return new WaitForSeconds(firingTime);

			isPlaying = false;
			isFired = false;
			
		}

	
	}
}
