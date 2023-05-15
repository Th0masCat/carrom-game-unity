using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTextPlayer1;

    [SerializeField]
    TextMeshProUGUI scoreTextPlayer2;

    public bool gameOver = false;
    bool isPaused = false;

    TimerScript timerScript;

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject gameOverMenu;


    [SerializeField]
    GameObject playerStriker;

    [SerializeField]
    GameObject enemyStriker;

    void Start()
    {
        Time.timeScale  = 1;
        BoardScript.scorePlayer1 = 0;
        BoardScript.scorePlayer2 = 0;
        timerScript = GetComponent<TimerScript>();
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame()
    {
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Time.timeScale = 1;
    }

    void Update()
    {


        if (StrikerController.playerTurn == true)
        {
            playerStriker.SetActive(true);
            enemyStriker.SetActive(false);
        }
        else
        {
            playerStriker.SetActive(false);
            enemyStriker.SetActive(true);
        }



        if (BoardScript.scorePlayer1 >= 8 || BoardScript.scorePlayer2 >= 8 || timerScript.timeLeft <= 0)
        {
            Debug.Log(timerScript.timeLeft);
            onGameOver();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !gameOver)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }


    void onGameOver()
    {
        gameOver = true;
        Time.timeScale = 0;
        BoardScript.scorePlayer1 = 0;
        BoardScript.scorePlayer2 = 0;
        gameOverMenu.SetActive(true);
        if (BoardScript.scorePlayer1 > BoardScript.scorePlayer2)
        {
            scoreTextPlayer1.text = "You Win!";
            scoreTextPlayer2.text = "You Lose!";
        }
        else if (BoardScript.scorePlayer1 < BoardScript.scorePlayer2)
        {
            scoreTextPlayer1.text = "You Lose!";
            scoreTextPlayer2.text = "You Win!";
        }
        else
        {
            scoreTextPlayer1.text = "Draw!";
            scoreTextPlayer2.text = "Draw!";
        }
    }

    private void LateUpdate()
    {
        if (!gameOver)
        {
            scoreTextPlayer1.text = BoardScript.scorePlayer1.ToString();
            scoreTextPlayer2.text = BoardScript.scorePlayer2.ToString();
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
