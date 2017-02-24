using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SequenceOfPlay : MonoBehaviour
{

    public Text scoreText;
    public GameObject sequenceDisplayText;
    public GameObject titleScreenCanvas;
    public GameObject battleScoreCanvas;

    public GameObject playerController;
    public GameObject viewFrame;

    public int playerCoins;

    private int playerScore = 0;
    public void AddScore(int score)
    {
        playerScore += score;
        if (scoreText)
        {
            scoreText.text = "$" + playerScore;
			//Debug.Log(scoreText.text);
        }
		//Debug.Log(playerScore);
    }

    void UpdateTextInPanel(GameObject canvas, string name, string value)
    {
        if (!canvas) return;
        foreach (Text child in canvas.GetComponentsInChildren<Text>())
        {
            if (child.name == name)
            {
                child.text = value;
            }
        }
    }

    void SetInteractability(GameObject canvas, bool value)
    {
        if (!canvas) return;
        foreach (Button child in canvas.GetComponentsInChildren<Button>())
        {
            child.interactable = value;
        }
    }

    public static SequenceOfPlay singleton;

    public void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
    }

    // Use this for initialization
    void Start()
    {
        if (sequenceDisplayText == null)
        {
            sequenceDisplayText = GameObject.Find("SequenceOfPlayInfoBox");
        }
        NextState();
    }

    public string[] sequenceOfPlay = new string[] { "ClearBattleField", "TitleScreen","GenerateBattleField",
        "BattleFieldFlyOver","Bidding","StartSimulation","EndSimulation","DistributeWin"};

    public int state = -1;
    public void NextState()
    {
        state++;
        state = state < sequenceOfPlay.Length ? state : 0;
        string stateText = sequenceOfPlay[state];
        if (sequenceDisplayText != null)
        {
            sequenceDisplayText.GetComponent<Text>().text = stateText;
            Debug.Log("State " + state + ": " + stateText);
            Invoke(stateText, 0f);
        }
        //Invoke ("NextState", 3f);  // TODO CHANGE THIS
    }


    void ClearBattleField()
    {
        Debug.Log("ClearBattleField");
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            item.GetComponent<CastleSpawnUtilities>().Despawn();
        }
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Projectile"))
        {
            Destroy(item);
        }
        if (titleScreenCanvas)
            titleScreenCanvas.SetActive(false);
        if (battleScoreCanvas)
            battleScoreCanvas.SetActive(false);

        //SetLauncherSpawn ( true );
        NextState();
    }

    void TitleScreen()
    {
        Debug.Log("TitleScreen");
        if (titleScreenCanvas)
            titleScreenCanvas.SetActive(true);
        //NextState();
    }

    void GenerateBattleField()
    {
        Debug.Log("GenerateBattleField");
        foreach (GameObject spawn in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawn.GetComponent<CastleSpawnUtilities>().Spawn();
        }

        NextState();
    }

    void BattleFieldFlyOver()
    {
        Debug.Log("BattleFieldFlyOver");
        //Camera.main.GetComponent<CameraZoom>().DoSpin();		
    }

    void StartSimulation()
    {
        Debug.Log("StartSimulation");
        if (battleScoreCanvas)
            battleScoreCanvas.SetActive(true);

        if ( playerController && viewFrame )
        {
            playerController.SetActive(true);
            viewFrame.GetComponent<Mover>().moving = true;
        }

        //SetLauncherSpawn ( true );
    }

    void EndSimulation()
    {
        Debug.Log("EndSimulation");
        //SetLauncherSpawn ( false );
        //Invoke ("NextState", 10.0f);
    }

    void DistributeWin()
    {
        Debug.Log("DistributeWin");
        if (battleScoreCanvas)
            battleScoreCanvas.SetActive(false);
        //Invoke ("NextState", 10.0f);
    }

}