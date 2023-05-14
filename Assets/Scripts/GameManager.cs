using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI scoreTextPlayer1;

    [SerializeField]
    TextMeshProUGUI scoreTextPlayer2;

    public bool gameOver = false;    

    TimerScript timerScript;

    void Start()
    {
        BoardScript.scorePlayer1 = 0;
        BoardScript.scorePlayer2 = 0;
        timerScript = GetComponent<TimerScript>();
    }

    private void LateUpdate()
    {
        scoreTextPlayer1.text = BoardScript.scorePlayer1.ToString();
        scoreTextPlayer2.text = BoardScript.scorePlayer2.ToString();


        if (timerScript.timeLeft <= 0)
        {
            gameOver = true;
            scoreTextPlayer1.text = "Time's Up!";
            scoreTextPlayer2.text = "Time's Up!";
        }
        else if (BoardScript.scorePlayer1 == 5)
        {
            gameOver = true;
            scoreTextPlayer1.text = "Player 1 Wins";
            scoreTextPlayer2.text = "Player 1 Wins";
        }
        else if (BoardScript.scorePlayer2 == 5)
        {
            gameOver = true;
            scoreTextPlayer1.text = "Player 2 Wins";
            scoreTextPlayer2.text = "Player 2 Wins";
        }
    }

    public void PlayerTurn(){
        Debug.Log("Player Turn");
    }

    public void ResetGame()
    {
        BoardScript.scorePlayer1 = 0;
        BoardScript.scorePlayer2 = 0;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
