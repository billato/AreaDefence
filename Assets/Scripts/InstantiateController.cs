using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class InstantiateController : MonoBehaviour {

	public int maxNoOfAttack = 1;
	public int currentNoOfAttack = 0;

	public int level = 0;
	public int enemyItems = 5;

	private float waitTimeUntilNext = 0.8f;

	public Transform[] enemy;

	GameObject respawnPosition;

	public GameObject[] attackersList = null;
	Button btn;

	public bool onAttack = false;

	GameOverManager gameOverManager;
	int attackerId;

	// Use this for initialization
	void Start () {

		btn = GameObject.Find("btnStartAttack").GetComponent<Button>();
		btn.GetComponentInChildren<Text>().text = "Start Attack " + (currentNoOfAttack+1) +"/"+maxNoOfAttack + "...";

		attackersList = null;

		if (level % 2 > 0) {
			attackerId = 1;
		} else {
			attackerId = 0;
		}

	}

	// Update is called once per frame
	void Update () {
	
		attackersList = GameObject.FindGameObjectsWithTag ("Target");

		//Debug.Log ("atList:" + attackersList.Length + "    current:" + currentNoOfAttack);


		if (attackersList != null && attackersList.Length > 0) {
			onAttack = true;
		}


			
		if (onAttack && attackersList.Length==0) {
			


			if (currentNoOfAttack >= maxNoOfAttack) {
				btn.interactable = false;
				btn.GetComponentInChildren<Text> ().text = "No attacks left.";


			} else {
				onAttack = false;
				btn.interactable = true;
				btn.GetComponentInChildren<Text> ().text = "Start Attack " + (currentNoOfAttack + 1) + "/" + maxNoOfAttack + "...";

			}
		}


		if (Input.GetKeyDown (KeyCode.R)) {
			StartCoroutine (respawn ());
		}
	

		
	}


	IEnumerator respawn(){


		for (int i = 0; i < (currentNoOfAttack*2)+1 + level ; i++) { 

			//pause time until next respawn
			yield return new WaitForSeconds (this.waitTimeUntilNext);



			respawnPosition = GameObject.Find("RespawnPosition");
			Instantiate (enemy[attackerId] , respawnPosition.transform.position , respawnPosition.transform.rotation);

		}
	}


	void OnGUI()
	{

		//   Paint();  
		 //startAttack();
	}


	public void startAttack(){
	
		//if (isButtonVisible && GUI.Button(buttonRectangle, "Start attack")){


		//disable Button Attack
		btn.interactable = false;
		btn.GetComponentInChildren<Text>().text = "attack in progress...";

		currentNoOfAttack++;

		//start attack
		StartCoroutine (respawn ());


	}

	public bool isAttacksFinished(){
		if (maxNoOfAttack < currentNoOfAttack) {
			return true;
		}

		return false;
	}


}
