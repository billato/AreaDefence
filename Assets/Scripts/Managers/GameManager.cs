﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager:MonoBehaviour{
	

    public static int score;

    Text text;

	// Use this for initialization
	void Start () {
	     text = GetComponent<Text>();
	     score = 0;
	}
	
	// Update is called once per frame
	void Update () {

		//update scoreboard
        text.text = "Score: " + score;

	}


	public int getScore(){
		return score;
	}

	public void increaseScore(int pointToAdd){
		score +=pointToAdd;
	}

	public void resetScore(){
		score=0;
	}

}