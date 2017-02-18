using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceOfPlay : MonoBehaviour {

	public GameObject sequenceDisplayText;
	public GameObject titleScreenCanvas;
	public GameObject biddingCanvas;
	public GameObject battleScoreCanvas;

	public int player1FlagCount = 0;
	public int player2FlagCount = 0;
	public GameObject player1CastleSpawn;
	public GameObject player2CastleSpawn;

	public void SwapPlayerCastles() {
		GameObject temp = player1CastleSpawn;
		player1CastleSpawn = player2CastleSpawn;
		player2CastleSpawn = temp;
	}

	int GetFlagCountForCastle( GameObject castle ) {
		return castle.GetComponent<CastleSpawnUtilities> ().FlagCount ();
	}

	public void UpdateFlagCount() {
		player1FlagCount = GetFlagCountForCastle (player1CastleSpawn);
		player2FlagCount = GetFlagCountForCastle (player2CastleSpawn);
		battleScoreCanvas.GetComponent<BattleScoreCanvasController> ().SetScore (player1FlagCount, player2FlagCount);
	}
			

	public static SequenceOfPlay singleton;

	public void Awake() {
		if (singleton == null) {
			singleton = this;
		}
	}
		
	// Use this for initialization
	void Start () {
		if (sequenceDisplayText == null) {
			sequenceDisplayText = GameObject.Find ("SequenceOfPlayInfoBox");
		}
		NextState ();		
	}

	public string[] sequenceOfPlay = new string[] { "ClearBattleField", "TitleScreen","GenerateBattleField",
		"BattleFieldFlyOver","Bidding","StartSimulation","EndSimulation","DistributeWin"};

	public int state = -1;
	public void NextState() {
		state++;
		state = state < sequenceOfPlay.Length ? state : 0;
		string stateText = sequenceOfPlay [state];
		if (sequenceDisplayText != null) {
			sequenceDisplayText.GetComponent<Text> ().text = stateText;
			Invoke (stateText, 0f);
		}
		//Invoke ("NextState", 3f);  // TODO CHANGE THIS
	}


	void ClearBattleField() {
		Debug.Log ("ClearBattleField");
		foreach (GameObject item in GameObject.FindGameObjectsWithTag("SpawnPoint")) {
			item.GetComponent<CastleSpawnUtilities> ().Despawn ();
		}
		foreach (GameObject item in GameObject.FindGameObjectsWithTag("Projectile")) {
			Destroy( item );
		}
		if (titleScreenCanvas)
			titleScreenCanvas.SetActive (false);
		if (biddingCanvas)
			biddingCanvas.SetActive (false);
		if (battleScoreCanvas)
			battleScoreCanvas.SetActive (false);

		SetLauncherSpawn ( true );
		NextState ();
	}

	void TitleScreen() {
		Debug.Log ("TitleScreen");
		if (titleScreenCanvas)
			titleScreenCanvas.SetActive (true);
		NextState ();
	}

	void GenerateBattleField() {
		Debug.Log ("GenerateBattleField");
		foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnPoint")) {
			spawn.GetComponent<CastleSpawnUtilities> ().Spawn ();
		}
		NextState ();
	}

	void BattleFieldFlyOver() {
		Debug.Log ("BattleFieldFlyOver");
		Camera.main.GetComponent<CameraZoom>().DoSpin();		
	}

	void Bidding() {
		Debug.Log ("Bidding");
		if (titleScreenCanvas)
			titleScreenCanvas.SetActive (false);
		if (biddingCanvas)
			biddingCanvas.SetActive (true);
		
	}

	void StartSimulation() {
		UpdateFlagCount ();
		Debug.Log ("StartSimulation");
		if (biddingCanvas)
			biddingCanvas.SetActive (false);
		if (battleScoreCanvas)
			battleScoreCanvas.SetActive (true);
		
		SetLauncherSpawn ( true );
	}

	void EndSimulation() {
		Debug.Log ("EndSimulation");
		SetLauncherSpawn ( false );
		//Invoke ("NextState", 10.0f);
	}

	void DistributeWin() {
		Debug.Log ("DistributeWin");
		if (battleScoreCanvas)
			battleScoreCanvas.SetActive (false);
		//Invoke ("NextState", 10.0f);
	}


	void SetLauncherSpawn( bool value ) {
		foreach (GameObject launcherSpawn in GameObject.FindGameObjectsWithTag("LauncherSpawner")) {
			launcherSpawn.GetComponent<LauncherActivity> ().activate_firing = value;
			Debug.Log ("Setting launcher ...");
		}
	}
}
