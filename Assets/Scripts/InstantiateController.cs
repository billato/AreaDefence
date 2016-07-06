using UnityEngine;
using System.Collections;

public class InstantiateController : MonoBehaviour {

	public int level = 0;

	private float waitTimeUntilNext = 0.7f;

	public Transform enemy;

	GameObject respawnPosition;


	// Use this for initialization
	void Start () {
		respawnPosition = GameObject.Find("RespawnPosition");
		StartCoroutine(respawn());

	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.R)) {
			Debug.Log ("R pressed...");

			StartCoroutine (respawn ());
		}
	}


	IEnumerator respawn(){

		for (int i = 0; i < (level * 2) + 3; i++) { 

			yield return new WaitForSeconds (this.waitTimeUntilNext);

			Debug.Log ("Calling instantiate...");


			Instantiate (enemy , respawnPosition.transform.position , respawnPosition.transform.rotation);

				

			}
		}

		

	}
