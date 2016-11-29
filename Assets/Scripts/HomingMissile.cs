using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour {

	public float missileVelocity = 50;
	public float turn = 10;
	public Rigidbody homingMissile;
	public float fuseDelay;
	public GameObject missileMod;
	public GameObject playerExplosion;

	private Transform target;



	// Use this for initialization
	void Start () {	
		homingMissile = transform.GetComponent<Rigidbody>();
		StartCoroutine(Fire ());
	}


	IEnumerator Fire(){

		yield return new WaitForSeconds(fuseDelay);

		float distance = Mathf.Infinity;

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("Target")){

			float diff = (go.transform.position - transform.position).sqrMagnitude;

			if(diff<distance){
				distance=diff;
				target = go.transform;
			}
		}
	}


	void OnTriggerEnter(Collider other){
		
		if (other.gameObject.CompareTag ("Terrain") || other.gameObject.CompareTag ("Target")) {
			//Debug.Log ("TERRRAIN touched!!!! by missile" + other.gameObject.tag);
			Instantiate (playerExplosion, this.transform.position, this.transform.rotation);
			Destroy (this.gameObject);
		}

	}




	// Update is called once per frame
	void Update () {
		
	}

	
	// Update is called once per frame
	void FixedUpdate () {
		if (target == null || homingMissile == null) {
			//push missile forward
			homingMissile.velocity = transform.forward * missileVelocity;
			return;
		}




		//push missile forward
		homingMissile.velocity = transform.forward * missileVelocity;

		Vector3 tmp = target.position - transform.position;
		tmp.y += 10; //target higher than the bottom of the attacker

		Quaternion targetRotation = Quaternion.LookRotation (tmp);
		homingMissile.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, turn));

	}




}
