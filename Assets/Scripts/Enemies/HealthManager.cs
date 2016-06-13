using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour{

	private int health = 100;

	public static int destroyBonus = 30;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void decreaseHealth(int damage){
		health -= damage;

		if (this.health <= 0) {
				Debug.Log ("Exploded enemy!!!!");
		}
	}




	void OnTriggerEnter(Collider other){

		if (other.gameObject.CompareTag ("Missile")) {

			decreaseHealth (20);

			//destroy missile
			Destroy (other.gameObject);


			if (this.health <= 0) {

				GameManager.score += destroyBonus;

			

				Destroy (this.gameObject);

				//GameManager.GetInstance ().increaseScore (destroyBonus);


			}



		}

	}

}


