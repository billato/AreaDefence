using UnityEngine;
using System.Collections;

public class Music : MonoBehaviour {
	private GameObject gameMusic;

	void Awake(){
		gameMusic = GameObject.Find ("BackgroundMusic");
	
		if (gameMusic) {
			//Destroy (gameMusic);	
		}
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (gameObject);
	
	}

}
