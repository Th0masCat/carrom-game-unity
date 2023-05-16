using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameOver = false;
    bool isPaused = false;

    [SerializeField]
    TextMeshProUGUI scoreTextEnemy;

    [SerializeField]
    TextMeshProUGUI scoreTextPlayer; 

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    GameObject gameOverMenu;

    [SerializeField]
    GameObject playerStriker;

    [SerializeField]
    GameObject enemyStriker;

    [SerializeField]
    GameObject turnText;

    [SerializeField]
    GameObject slider;

    [SerializeField]
    TextMeshProUGUI gameOverText;

    [SerializeField]
    Animator animator;

    TimerScript timerScript;

    void Start()
    {
        Time.timeScale = 1;
        BoardScript.scoreEnemy = 0;
        BoardScript.scorePlayer = 0;
        timerScript = GetComponent<TimerScript>();
    }

    void Update()
    {
        if (StrikerController.playerTurn == true)
        {
            slider.SetActive(true);
            turnText.SetActive(true);
            playerStriker.SetActive(true);
            enemyStriker.SetActive(false);
        }
        else
        {
            slider.SetActive(false);
            turnText.SetActive(false);
            playerStriker.SetActive(false);
            enemyStriker.SetActive(true);
        }

        if (BoardScript.scoreEnemy >= 8 || BoardScript.scorePlayer >= 8 || timerScript.timeLeft <= 0)
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

    private void LateUpdate()
    {
        if (!gameOver)
        {
            scoreTextEnemy.text = BoardScript.scoreEnemy.ToString();
            scoreTextPlayer.text = BoardScript.scorePlayer.ToString();
        }
    }

    IEnumerator playAnimation()
    {
        animator.SetTrigger("fade");
        yield return new WaitForSeconds(1f);

    }

    void onGameOver()
    {
        gameOver = true;
        gameOverMenu.SetActive(true);
        Time.timeScale = 0;
        if (BoardScript.scoreEnemy > BoardScript.scorePlayer)
        {
            gameOverText.text = "You Lose!";
        }
        else if (BoardScript.scoreEnemy < BoardScript.scorePlayer)
        {
            gameOverText.text = "You Win!";
        }
        else
        {
            gameOverText.text = "Draw!";
        }
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
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        StartCoroutine(playAnimation());
        SceneManager.LoadScene(0);
    }
}
