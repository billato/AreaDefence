using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerHealthManager : MonoBehaviour {

	public static int maxHealth = 10;
	public int currentHealth;

	Text lifeText;

	// Use this for initialization
	void Start () {
		
		currentHealth = maxHealth ;
		lifeText = GameObject.Find("LifeText").GetComponent<Text>();

		lifeText.text = "" + maxHealth;
	}
	

	void OnTriggerEnter(Collider other){

		if (other.transform.tag == "Target") {
			decreaseHealth ();
		}
	}

	void decreaseHealth(){
		if (currentHealth > 0) {
			currentHealth--;
			lifeText.text = "" + currentHealth;
		}
	}

	public int getCurrentHealth(){
		return currentHealth;
	}


}
