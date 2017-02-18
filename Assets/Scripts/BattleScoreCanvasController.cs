using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScoreCanvasController : MonoBehaviour {

	public Text player1Score = null;
	public Text player2Score = null;

	// Use this for initialization
	void Start () {
		SetScore (1, 3);
	}

	string ScoreToString( int score ) {
		if (score > 0) {
			return "+" + score;
		} else {
			return "" + score;
		}
	}

	public void SetScore( int flagsPlayer1Lost , int flagsPlayer2Lost ) {
		if (player1Score != null && player2Score != null) {
			player1Score.text = ScoreToString( flagsPlayer1Lost - flagsPlayer2Lost );
			player2Score.text = ScoreToString( flagsPlayer2Lost - flagsPlayer1Lost );
		}
	}

}
