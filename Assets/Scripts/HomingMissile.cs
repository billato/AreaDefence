using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour {

	public float missileVelocity = 300;
	public 	float turn = 20;
	public Rigidbody homingMissile;
	public float fuseDelay;
	public GameObject missileMod;

	private Transform target;



	// Use this for initialization
	void Start () {	
		homingMissile = transform.GetComponent<Rigidbody>();
		StartCoroutine(Fire ());
	}


	IEnumerator Fire(){

		yield return new WaitForSeconds(fuseDelay);

		float distance = Mathf.Infinity;

		foreach (GameObject go in GameObject.FindGameObjectsWithTag("target")){

			float diff = (go.transform.position - transform.position).sqrMagnitude;

			if(diff<distance){
				distance=diff;
				target = go.transform;
			}
		}
	}

	// Update is called once per frame
	void Update () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		if (target == null || homingMissile == null) 
			return;
		
		//push missile forward
		homingMissile.velocity = transform.forward * missileVelocity;

		Vector3 tmp = target.position - transform.position;

		Quaternion targetRotation = Quaternion.LookRotation (tmp);
		homingMissile.MoveRotation (Quaternion.RotateTowards (transform.rotation, targetRotation, turn));

	}




}
