using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TurretPerimeter : MonoBehaviour {

	public AudioClip[] sounds;

	public int moneyCost;

	private bool isPlaying = false;
	private bool isFired = false;

	private float delayToLockEnemy = 0.5f;
	private float firingTime = 0.5f;
	public int speed = 100;


	Vector3 m_lastKnownPosition = Vector3.zero;
	Quaternion m_lookAtRotation;

	float facingAngle = 30.0f;

	public Rigidbody missile;

	List <Transform> enemiesInRange = new List<Transform>();
	Transform lockedEnemy= null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(lockedEnemy==null &&enemiesInRange.Count>0){
			enemiesInRange.RemoveAt(0);
		}


	}


	IEnumerator OnTriggerEnter(Collider other){
		
		if (other.gameObject.CompareTag("Target")){
		
			// we have no lock on enemy
			if (lockedEnemy == null) {
				// lock on the entering enemy
					lockedEnemy = other.transform; // other.gameObject.transform;
			}
			// Add it to the list of enemies
			enemiesInRange.Add(other.transform);

			//other.gameObject.SetActive (false);
			//GameManager.GetInstance ().increaseScore();

			yield return new WaitForSeconds(delayToLockEnemy);

			StartCoroutine(fire());
			//StartCoroutine(fire());

		}




	}




	void OnTriggerStay(Collider other){


		if (other.gameObject.CompareTag ("Target") && lockedEnemy!=null) {
			

			if(m_lastKnownPosition != other.gameObject.transform.position){
				m_lastKnownPosition = other.gameObject.transform.position;
				m_lookAtRotation = Quaternion.LookRotation(m_lastKnownPosition - transform.position);
			}

			if (transform.rotation != m_lookAtRotation) {
				//transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookAtRotation, speed * Time.deltaTime);

				this.transform.GetChild (2).rotation = Quaternion.RotateTowards (this.transform.GetChild (2).rotation, m_lookAtRotation, speed * Time.deltaTime);
				this.transform.GetChild (0).eulerAngles = new Vector3 (0, this.transform.GetChild (2).eulerAngles.y, 0);
				this.transform.GetChild (3).eulerAngles = new Vector3 (0, this.transform.GetChild (2).eulerAngles.y, 0);
			} 

			StartCoroutine(fire());

			/*
			//BACKUP 

			//turret head follow the enemy
			this.transform.GetChild (2).LookAt  (other.transform.position); //Turret head looking at enemy 
			//turret base rotate to the enemy
			this.transform.GetChild (0).eulerAngles = new Vector3(0,this.transform.GetChild (2).eulerAngles.y,0);
			this.transform.GetChild (3).eulerAngles = new Vector3(0,this.transform.GetChild (2).eulerAngles.y,0);

			StartCoroutine(fire());
			*/



			/*
			//Camera head looking at enemy 
			this.transform.GetChild (4).eulerAngles  = new Vector3(
				this.transform.GetChild (2).eulerAngles.x,
				this.transform.GetChild (2).eulerAngles.y,
				this.transform.GetChild (2).eulerAngles.z); 
			*/
		}






	}


	void OnTriggerExit(Collider other) {
		if (lockedEnemy == other.transform) {
			// if the enemy leaving the range is the one we're targeting remove it from the enemies in range
			lockedEnemy = null;

		}

		enemiesInRange.Remove(other.transform);

		// reacquire target from the enemies in range (the first one in the list).
		if (enemiesInRange.Count > 0) {
			lockedEnemy = enemiesInRange[0];
		} else {
			// if no enemies are in range , set the locked enemy to null
			lockedEnemy = null;
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

	public int getMoneyCost(){
		return moneyCost;
	}


}
