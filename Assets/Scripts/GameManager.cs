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

        
            if(StrikerController.playerTurn == true ){
                playerStriker.GetComponent<StrikerController>().enabled = true;
                enemyStriker.GetComponent<EnemyStrikerController>().enabled = false;
            }else{
                playerStriker.GetComponent<StrikerController>().enabled = false;
                enemyStriker.GetComponent<EnemyStrikerController>().enabled = true;
            }
        


        if(timerScript.isTimerRunning == false || BoardScript.scorePlayer1 == 5 || BoardScript.scorePlayer2 == 5){
            onGameOver();
        }
        
        if(Input.GetKeyDown(KeyCode.Escape) && !gameOver){
            if(isPaused){
                ResumeGame();
            }else{
                PauseGame();
            }
        }
    }


    void onGameOver(){
        gameOver = true;    
        Time.timeScale = 0;
        BoardScript.scorePlayer1 = 0;
        BoardScript.scorePlayer2 = 0;
        gameOverMenu.SetActive(true);
        if(BoardScript.scorePlayer1 > BoardScript.scorePlayer2){
            scoreTextPlayer1.text = "You Win!";
            scoreTextPlayer2.text = "You Lose!";
        }else if(BoardScript.scorePlayer1 < BoardScript.scorePlayer2){
            scoreTextPlayer1.text = "You Lose!";
            scoreTextPlayer2.text = "You Win!";
        }else{
            scoreTextPlayer1.text = "Draw!";
            scoreTextPlayer2.text = "Draw!";
        }
    }

    private void LateUpdate()
    {   
        if(!gameOver)
        {scoreTextPlayer1.text = BoardScript.scorePlayer1.ToString();
        scoreTextPlayer2.text = BoardScript.scorePlayer2.ToString();}
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
