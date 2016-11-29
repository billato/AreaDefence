using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour{


	public AudioClip[] sounds;

	public int maxHealth;
	private float currentHealth;

	public GameObject healthBar;
	public GameObject currentHealthBar;

	public int destroyBonus = 40;

	// Use this for initialization
	void Start () {
		
		currentHealth = maxHealth;

		healthBar.transform.position = new Vector3 (
			this.gameObject.transform.position.x,
			this.gameObject.transform.position.y+13,
			this.gameObject.transform.position.z
		);
	}
	
	// Update is called once per frame
	void Update () {
		healthBar.transform.position = new Vector3 (
			this.gameObject.transform.position.x,
			this.gameObject.transform.position.y+13,
			this.gameObject.transform.position.z
		);
	}



	// Health between [0.0f,1.0f] == (currentHealth / totalHealth)
	public void SetHealthVisual(float healthNormalized){
		currentHealthBar.transform.localScale = new Vector3( healthNormalized,
			currentHealthBar.transform.localScale.y,
			currentHealthBar.transform.localScale.z);
	}


	public void decreaseHealth(int damage){
		currentHealth -= damage;

		float calcHealth = currentHealth / maxHealth; // if curr 80 / 100 = 0.8f
		SetHealthVisual(calcHealth);

		if (this.currentHealth <= 0) {
				Debug.Log ("Exploded enemy!!!!");

		}


	}




	void OnTriggerEnter(Collider other){

		if (other.gameObject.CompareTag ("Missile")) {

			decreaseHealth (20);

			//destroy missile
			Destroy (other.gameObject);

			if (this.currentHealth <= 0) {

				GameManager.score += destroyBonus;

				//play sound each time token is picked up
				AudioSource.PlayClipAtPoint (sounds [0], transform.position);

				Destroy (this.gameObject);





				//GameManager.GetInstance ().increaseScore (destroyBonus);
			}


		}

		if (other.gameObject.CompareTag ("NavMeshAgentTarget")) {
			
			Destroy (this.gameObject);




		}

	}

}


