using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    
    private bool gameStarted ;
    public bool GameStarted
    {
        get { return gameStarted;  }
    }

    private bool gameFinished;
    public bool GameFinished
    {
        get { return gameFinished; }
    }
    [SerializeField]
    private GameObject startGameMenu;
    [SerializeField]
    private GameObject pauseGameMenu;
    [SerializeField]
    private GameObject loseGameMenu;
    private GameObject playerTower;
    public static GameController instance;

    // Use this for initialization
	void Start () {
        InitializeGameController();
	}
    private void Update()
    {
        if(!gameFinished && gameStarted && Input.GetButtonDown("Cancel"))
        {
            if (pauseGameMenu.activeSelf)
            {
                UnPauseGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void StartGame()
    {
        startGameMenu.SetActive(false);
        gameStarted = true;
        ScoreManager.instance.InitializeScore();
        EnemyManager.instance.StartSpawningEnemies();
    }

    public void FinishGame()
    {
        gameFinished = true;
        ScoreManager.instance.SetEndGameScore();
        EnemyManager.instance.AddAllEnemiesToPool();
        loseGameMenu.SetActive(true);
    }
    public void Restart()
    {
        RestartGame();
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void UnPauseGame()
    {
        pauseGameMenu.SetActive(false);
        Time.timeScale = 1;
    }
    private void PauseGame()
    {
        pauseGameMenu.SetActive(true);
        Time.timeScale = 0;
    }
    private void RestartGame()
    {
        gameStarted = false;
        gameFinished = false;
        loseGameMenu.SetActive(false);
        startGameMenu.SetActive(true);
    }
    private void InitializeGameController()
    {
        if (instance == null)
        {
            instance = this;
        }
        gameStarted = false;
        gameFinished = false;
        startGameMenu.SetActive(true);
        playerTower = GameObject.FindGameObjectWithTag("Player");
        playerTower.GetComponent<Tower>().isDead += FinishGame;
    }

    private void OnApplicationQuit()
    {
        playerTower.GetComponent<Tower>().isDead -= FinishGame;
    }
}
