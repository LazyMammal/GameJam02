using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleScoreCanvasController : MonoBehaviour {

	public Text playerScore = null;

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
		int coins = SequenceOfPlay.singleton.playerCoins;

		if (playerScore != null) {
			playerScore.text = ScoreToString( coins );
		}
	}

}
