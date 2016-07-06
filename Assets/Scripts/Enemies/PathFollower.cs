using UnityEngine;
using System.Collections;


public class PathFollower: MonoBehaviour {
	public Transform[] path;
	public float speed = 5.0f;
	public float rotationSpeed = 1.0f;
	public float reachDist = 1.0f;
	public int currentPoint = 0;

	Quaternion neededRotation;
	Quaternion interpolatedRotation;
	Quaternion yourRotation;


	// Use this for initialization
	void Start () {


	
	}
	
	// Update is called once per frame
	void Update () {

		float dist = Vector3.Distance (path [currentPoint].position, transform.position);


		transform.LookAt( Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime * rotationSpeed) );
		transform.Translate (0, 0, rotationSpeed * Time.deltaTime);


		transform.position = Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime * speed);

		//Debug.Log( Vector3.MoveTowards (transform.position, path [currentPoint].position, Time.deltaTime * speed));
			
		if (dist <= reachDist) {
			currentPoint++;
		} else {
		

		
		}
		if (currentPoint >= path.Length) {
			//currentPoint = 0;
			Destroy(this.gameObject);
		}
	}


	//Create gizmos for path
	  void onDrawGizmos(){
		if (path.Length > 0) {
			for (int i = 0; i < path.Length; i++) {
				if (path [i] != null) {
					Gizmos.DrawSphere (path [i].position, reachDist);
				}
			}
		}
	}




}
