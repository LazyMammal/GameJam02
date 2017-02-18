using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceOfPlay : MonoBehaviour {

	public GameObject sequenceDisplayText = null;

	// Use this for initialization
	void Start () {
		if (sequenceDisplayText == null) {
			sequenceDisplayText = GameObject.Find ("SequenceOfPlayInfoBox");
		}
		NextState ();		
	}

	public string[] sequenceOfPlay = new string[] { "ClearBattleField", "TitleScreen","GenerateBattleField",
		"BattleFieldFlyOver","Bidding","StartSimulation","DistributeWin"};

	public int state = -1;
	public void NextState() {
		state++;
		state = state < sequenceOfPlay.Length ? state : 0;
		string stateText = sequenceOfPlay [state];
		if (sequenceDisplayText != null) {
			sequenceDisplayText.GetComponent<Text> ().text = stateText;
			Invoke (stateText, 0f);
		}
		Invoke ("NextState", 1f);
	}


	void ClearBattleField() {
		Debug.Log ("ClearBattleField");
	}

	void TitleScreen() {
		Debug.Log ("TitleScreen");
	}

	void GenerateBattleField() {
		Debug.Log ("GenerateBattleField");
	}

	void BattleFieldFlyOver() {
		Debug.Log ("BattleFieldFlyOver");
	}

	void Bidding() {
		Debug.Log ("Bidding");
	}

	void StartSimulation() {
		Debug.Log ("StartSimulation");
	}

	void EndSimulation() {
		Debug.Log ("EndSimulation");
	}

	void DistributeWin() {
		Debug.Log ("DistributeWin");
	}


}
