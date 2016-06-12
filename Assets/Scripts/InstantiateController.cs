using UnityEngine;
using System.Collections;

public class InstantiateController : MonoBehaviour {

	public int level = 0;

	private float waitTimeUntilNext = 1.0f;

	public Rigidbody enemy;

	GameObject respawnPosition;


	// Use this for initialization
	void Start () {
		respawnPosition = GameObject.Find("RespawnPosition");
		StartCoroutine(respawn());

	}
	
	// Update is called once per frame
	void Update () {
	

	}


	IEnumerator respawn(){
	
		
		
		for (int i = 0; i < (level * 2) + 1; i++) { 
				Instantiate (enemy
					, respawnPosition.transform.position 
					, respawnPosition.transform.rotation);

				yield return new WaitForSeconds (this.waitTimeUntilNext);
			}
		}

		

	}
