using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager:MonoBehaviour{
	
	/* 
	 * 0: menu, 
	 * 1: user positioning turrets
	 * 2: user plays level
	 * 3. game ends
	*/
	public int gameState;
	private int level;

    public static int score;
	Text[] textList;
	Text scoreText;

	// Use this for initialization
	void Start () {
		score = 130;
		level = 1;

		scoreText = GameObject.Find ("ScoreText").GetComponent<Text> ();

	}
	
	// Update is called once per frame
	void Update () {

		// Exit game if esc pressed
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}


		if (scoreText != null) {
			//update scoreboard
			scoreText.text = "" + score;
		}


	}


	public int getScore(){
		return score;
	}

	public void increaseScore(int points){
		score += points;
	}

	public void descreaseScore(int points){
		score -= points;
	}

	public void resetScore(){
		score=0;
	}


	public void increaseLevel(){
		level++;
	}


	public void reloadLevel(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);

	}

	public void loadLevel(int levelIndex){
		SceneManager.LoadScene (levelIndex);

	}


	public void loadNextLevel(){

		int tt = SceneManager.sceneCount;

		if((SceneManager.sceneCount-1) < level+1){
			level++;
			SceneManager.LoadScene (level);
		}else{
			level = 0;
			SceneManager.LoadScene(0);
		}

	}




}
