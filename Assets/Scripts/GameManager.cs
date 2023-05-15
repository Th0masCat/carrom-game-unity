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

    void Start()
    {
        BoardScript.scorePlayer1 = 0;
        BoardScript.scorePlayer2 = 0;
        timerScript = GetComponent<TimerScript>();
    }

    public void ResumeGame(){
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void PauseGame(){
        isPaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void RestartGame(){
        StrikerController.playerTurn = true;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(isPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
        }
    }

    private void LateUpdate()
    {
        scoreTextPlayer1.text = BoardScript.scorePlayer1.ToString();
        scoreTextPlayer2.text = BoardScript.scorePlayer2.ToString();

        if (timerScript.timeLeft <= 0)
        {
            gameOver = true;
        }
        else if (BoardScript.scorePlayer1 == 8)
        {
            gameOver = true;
            scoreTextPlayer1.text = "Player 1 Wins";
            scoreTextPlayer2.text = "Player 1 Wins";
        }
        else if (BoardScript.scorePlayer2 == 8)
        {
            gameOver = true;
            scoreTextPlayer1.text = "Player 2 Wins";
            scoreTextPlayer2.text = "Player 2 Wins";
        }
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
