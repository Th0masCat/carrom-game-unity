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

    

    private void LateUpdate()
    {
        scoreTextPlayer1.text = BoardScript.scorePlayer1.ToString();
        scoreTextPlayer2.text = BoardScript.scorePlayer2.ToString();
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
