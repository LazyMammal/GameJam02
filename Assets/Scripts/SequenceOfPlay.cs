using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SequenceOfPlay : MonoBehaviour
{

    public Text scoreText;
    public GameObject sequenceDisplayText;
    public GameObject titleScreen;
    private GameObject titleScreenCanvas;
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

        titleScreenCanvas = GameObject.Find("TitleCanvas");

        NextState();
    }

    public string[] sequenceOfPlay = new string[] { "ClearBattleField", "TitleScreen","GenerateBattleField",
        "BattleFieldFlyOver","Bidding","StartSimulation","EndSimulation","DistributeWin"};

    public int state = -1;
    public void NextState(int goToState = -1)
    {
        state = goToState >= 0 ? goToState : state + 1;
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
        if (titleScreen)
            titleScreen.SetActive(false);
        if (battleScoreCanvas)
            battleScoreCanvas.SetActive(false);

        NextState();
    }

    void TitleScreen()
    {
        Cursor.visible = true;
        Debug.Log("TitleScreen");
        if (battleScoreCanvas)
            battleScoreCanvas.SetActive(false);
        if (titleScreen)
            titleScreen.SetActive(true);
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

    void RestartEverything()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void QuitGame()
    {
        Application.Quit();
    }

    void StartSimulation()
    {
        Debug.Log("StartSimulation");

        Cursor.visible = false;

        if (titleScreen)
        {
            titleScreen.SetActive(false);
            titleScreenCanvas.transform.Find("StartButton").GetComponentInChildren<Text>().text = "RESUME";
            //titleScreenCanvas.transform.Find("RestartButton").gameObject.SetActive(true);
        }
        if (battleScoreCanvas)
            battleScoreCanvas.SetActive(true);

        if (playerController && viewFrame)
        {
            playerController.SetActive(true);
            viewFrame.GetComponent<Mover>().moving = true;
        }

        if (titleScreen)
            titleScreen.transform.Find("Truck").gameObject.SetActive(false);

        Time.timeScale = 1f;

        //SetLauncherSpawn ( true );
    }

    void DistributeWin()
    {
        Debug.Log("DistributeWin");
        if (battleScoreCanvas)
            battleScoreCanvas.SetActive(false);
        //Invoke ("NextState", 10.0f);
    }

}