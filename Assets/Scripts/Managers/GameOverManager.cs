using UnityEngine;
using System.Collections;

public class GameOverManager : MonoBehaviour {

	public PlayerHealthManager playerHealthManager; 
	public InstantiateController instantiateController;

	Animator anim;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void Update () {
	
		if (playerHealthManager.getCurrentHealth() <= 0) {
			anim.SetTrigger ("GameOver");
		}


		if (instantiateController.onAttack && 
			instantiateController.attackersList.Length == 0 &&
			instantiateController.currentNoOfAttack >= instantiateController.maxNoOfAttack) {
		
			anim.SetTrigger ("Win");
		
		}





	}
}
