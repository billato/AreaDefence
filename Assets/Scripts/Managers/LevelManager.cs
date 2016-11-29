using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	private int level = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void LoadLevel(int level){
		SceneManager.LoadScene (level);
	}

	public void ExitApplication(){
		Application.Quit();
	}
}
