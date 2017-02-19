using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceOfPlay : MonoBehaviour {

	public GameObject cameraPivot;

	public GameObject sequenceDisplayText;
	public GameObject titleScreenCanvas;
	public GameObject biddingCanvas;
	public GameObject battleScoreCanvas;

	public int player1FlagCount = 0;
	public int player2FlagCount = 0;
	public GameObject player1CastleSpawn;
	public GameObject player2CastleSpawn;

	public GameObject player1BiddingPanel;
	public GameObject player2BiddingPanel;

	public GameObject distributeWinCanvas;
	public Text player1WinDisplay;
	public Text player2WinDisplay;

	public int player1Coins = 500;
	public int player2Coins = 500;

	string castleSwitchText = "Alter the facts\nCost : ";

	public int costToSwap = 0;
	public void increaseSwapPrice() {
		costToSwap = (costToSwap == 0) ? 10 : costToSwap*2;
	}

	public int playerBidding = 1;




	void UpdateTextInPanel(GameObject playerBiddingPanel, string name, string value) {
		foreach ( Text child in playerBiddingPanel.GetComponentsInChildren<Text>() ) {
			if ( child.name == name ) {
				child.text = value;
			}
		}
	}

	void SetInteractability(GameObject playerBiddingPanel, bool value, bool hasMoney) {
		foreach ( Button child in playerBiddingPanel.GetComponentsInChildren<Button>() ) {
			child.interactable = value;
			if (child.name == "OtherCastle" && !hasMoney) {
				child.interactable = false;
			}
		}
	}

	public void GlobalIWantThisCastle() {
		singleton.IWantThisCastle ();
	}

	public void UpdateBiddingPanelVisibility() {
		SetInteractability (player1BiddingPanel, playerBidding == 1 , player1Coins > costToSwap );
		SetInteractability (player2BiddingPanel, playerBidding == 2 , player2Coins > costToSwap );
	}

	public void IWantThisCastle() {
		playerBidding = playerBidding == 1 ? 2 : 1;
		UpdateBiddingPanelVisibility ();
		UpdatePlayCoins ();
		NextState ();
	}

	public void GlobalSwapCastles() {
		singleton.SwapCastles ();
	}

	public void SwapCastles() {
		player1Coins += playerBidding == 1 ? -costToSwap : costToSwap;
		player2Coins += playerBidding == 2 ? -costToSwap : costToSwap;
		increaseSwapPrice ();
		SwapPlayerCastles ();
		playerBidding = playerBidding == 1 ? 2 : 1;
		UpdatePlayCoins ();
		UpdateFlagCount ();
		UpdateBiddingPanelVisibility ();
		cameraPivot.GetComponent<CameraTool>().Spin(180);
	}

	public void UpdatePlayCoins() {
		UpdateTextInPanel (player1BiddingPanel, "CoinsDisplay", "" + player1Coins);
		UpdateTextInPanel (player2BiddingPanel, "CoinsDisplay", "" + player2Coins);
		UpdateTextInPanel (player1BiddingPanel, "OtherCastleText", castleSwitchText + costToSwap);
		UpdateTextInPanel (player2BiddingPanel, "OtherCastleText", castleSwitchText + costToSwap);
	}


	public void SwapPlayerCastles() {
		GameObject temp = player1CastleSpawn;
		player1CastleSpawn = player2CastleSpawn;
		player2CastleSpawn = temp;
	}

	int GetFlagCountForCastle( GameObject castle ) {
		return castle.GetComponent<CastleSpawnUtilities> ().FlagCount ();
	}

	public void DelayedUpdateFlagCount() {
		Invoke ("UpdateFlagCount", 0.3f);
	}

	public void UpdateFlagCount() {
		player1FlagCount = GetFlagCountForCastle (player1CastleSpawn);
		player2FlagCount = GetFlagCountForCastle (player2CastleSpawn);
		battleScoreCanvas.GetComponent<BattleScoreCanvasController> ().SetScore();
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
		if (distributeWinCanvas)
			distributeWinCanvas.SetActive (false);

		SetLauncherSpawn ( true );
		UpdatePlayCoins ();
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
		UpdateFlagCount ();
		NextState ();
	}

	void BattleFieldFlyOver() {
		Debug.Log ("BattleFieldFlyOver");
		cameraPivot.GetComponent<CameraTool>().Spin(360);
		UpdateFlagCount ();
		Invoke ("SwitchFromBattleFieldFlyOver", 25.0f);
	}
	void SwitchFromBattleFieldFlyOver() {
		if (sequenceOfPlay [state] == "BattleFieldFlyOver")
			NextState();
	}

	void Bidding() {
		Debug.Log ("Bidding");
		UpdateFlagCount ();
		if (player1FlagCount == 0 || player2FlagCount == 0) {
			NextState ();
		}
		if (battleScoreCanvas)
			battleScoreCanvas.SetActive (false);
		if (titleScreenCanvas)
			titleScreenCanvas.SetActive (false);
		if (biddingCanvas)
			biddingCanvas.SetActive (true);
		UpdateBiddingPanelVisibility ();
		
	}

	void StartSimulation() {
		Debug.Log ("StartSimulation");
		if (biddingCanvas)
			biddingCanvas.SetActive (false);
		if (battleScoreCanvas)
			battleScoreCanvas.SetActive (true);
		UpdateFlagCount ();
		if (player1FlagCount > 0 && player2FlagCount > 0) {
			SetLauncherSpawn (true);
			SpawnAngryPills ();
			Invoke ("SwitchFromStartSimulation", 3.0f);
		} else {
			NextState ();
		}
	}
	void SwitchFromStartSimulation() {
		if (sequenceOfPlay [state] == "StartSimulation")
			NextState();
	}

	void EndSimulation() {
		Debug.Log ("EndSimulation");
		SetLauncherSpawn ( false );
		UpdateFlagCount ();
		DelayedUpdateFlagCount ();
		if (player1FlagCount > 0 && player2FlagCount > 0) {
			Invoke ("SwitchFromEndSimulation", 3.0f);
		} else {
			NextState ();
		}
	}
	void SwitchFromEndSimulation() {
		if (sequenceOfPlay [state] == "EndSimulation")
			NextState();
	}

	void DistributeWin() {
		Debug.Log ("DistributeWin");
		if (battleScoreCanvas)
			battleScoreCanvas.SetActive (false);
		costToSwap = 0;

		player1WinDisplay.text = "" + ( player1FlagCount > player2FlagCount ? 1000 : 1);
		player2WinDisplay.text = "" + ( player2FlagCount > player1FlagCount ? 1000 : 1);
		player1Coins += player1FlagCount > player2FlagCount ? 1000 : 1;
		player2Coins += player2FlagCount > player1FlagCount ? 1000 : 1;

		if (distributeWinCanvas)
			distributeWinCanvas.SetActive (true);

		Invoke ("SwitchFromDistributeWin", 5.0f);
	}
	void SwitchFromDistributeWin() {
		if (sequenceOfPlay [state] == "DistributeWin")
			NextState();
	}


	void SetLauncherSpawn( bool value ) {
		foreach (GameObject launcherSpawn in GameObject.FindGameObjectsWithTag("LauncherSpawner")) {
			launcherSpawn.GetComponent<LauncherActivity> ().activate_firing = value;
		}
	}

	void SpawnAngryPills() {
		player1CastleSpawn.GetComponent<PlacePills> ().Spawn ();
		player2CastleSpawn.GetComponent<PlacePills> ().Spawn ();
	}
}
