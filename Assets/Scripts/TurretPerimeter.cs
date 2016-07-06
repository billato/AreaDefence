using UnityEngine;
using System.Collections;

public class TurretPerimeter : MonoBehaviour {

	public AudioClip[] sounds;

	private bool isPlaying = false;
	private bool isFired = false;

	private float delayToLockEnemy = 0.5f;
	private float firingTime = 1.2f;

	public Rigidbody missile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator OnTriggerEnter(Collider other){
		
		if (other.gameObject.CompareTag("target")){
			//other.gameObject.SetActive (false);
			//GameManager.GetInstance ().increaseScore();
					
			yield return new WaitForSeconds(delayToLockEnemy);

			//StartCoroutine(fire());

		}
	}


	void OnTriggerStay(Collider other){

		if (other.gameObject.CompareTag ("target")) {
			

			//turret head follow the enemy
			this.transform.GetChild (2).LookAt  (other.transform.position); //Turret head looking at enemy 
			//turret base rotate to the enemy
			this.transform.GetChild (0).eulerAngles = new Vector3(0,this.transform.GetChild (2).eulerAngles.y,0);
			this.transform.GetChild (3).eulerAngles = new Vector3(0,this.transform.GetChild (2).eulerAngles.y,0);

			StartCoroutine(fire());


			/*
			//Camera head looking at enemy 
			this.transform.GetChild (4).eulerAngles  = new Vector3(
				this.transform.GetChild (2).eulerAngles.x,
				this.transform.GetChild (2).eulerAngles.y,
				this.transform.GetChild (2).eulerAngles.z); 
			*/
		}
	}


	IEnumerator fire(){
		if (isPlaying==false || isFired ==false) {
			
			isPlaying = true;
			isFired = true;


			//play sound each time token is picked up
			AudioSource.PlayClipAtPoint (sounds [0], transform.position);

			Instantiate (missile
				, this.transform.GetChild (2).GetChild (0).position 
				,this.transform.GetChild (2).rotation);

			yield return new WaitForSeconds(firingTime);


			isPlaying = false;
			isFired = false;
			
		}
	}


}
