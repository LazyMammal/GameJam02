using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScoreCanvasController : MonoBehaviour {

	public Text player1Score = null;
	public Text player2Score = null;

	// Use this for initialization
	void Start () {
		SetScore ();
	}

	string ScoreToString( int score ) {
		if (score > 0) {
			return "+" + score;
		} else {
			return "" + score;
		}
	}

	public void SetScore() {
		int flagsPlayer1Flags = SequenceOfPlay.singleton.player1FlagCount;
		int flagsPlayer2Flags = SequenceOfPlay.singleton.player2FlagCount;

		if (player1Score != null && player2Score != null) {
			player1Score.text = ScoreToString( flagsPlayer1Flags - flagsPlayer2Flags );
			player2Score.text = ScoreToString( flagsPlayer2Flags - flagsPlayer1Flags );
		}
	}

}
