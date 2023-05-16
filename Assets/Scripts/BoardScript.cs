using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardScript : MonoBehaviour
{
    public static int scoreEnemy = 0;
    public static int scorePlayer = 0;

    TextMeshProUGUI popUpText;

    private void Start()
    {
        popUpText = GameObject.Find("UpdatesText").GetComponent<TextMeshProUGUI>();
    }
   
    IEnumerator textPopUp(string text)
    {
        popUpText.text = text;
        popUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        popUpText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Striker":
                if (StrikerController.playerTurn == true)
                {
                    scorePlayer--;
                }
                else
                {
                    scoreEnemy--;
                }

                StartCoroutine(textPopUp("Striker Lost: -1 to " + (StrikerController.playerTurn ? "Player" : "Enemy")));
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case "Black":
                scoreEnemy++;

                StartCoroutine(textPopUp("Black Coin Entered: +1 to Enemy"));
                Destroy(other.gameObject);
                break;
            case "White":
                scorePlayer++;

                StartCoroutine(textPopUp("White Coin Entered: +1 to Player"));
                Destroy(other.gameObject);
                break;
            case "Queen":

                if (StrikerController.playerTurn == true)
                {
                    scorePlayer += 2;
                }
                else
                {
                    scoreEnemy += 2;
                }

                StartCoroutine(textPopUp("Queen Entered: +2 to " + (StrikerController.playerTurn ? "Player" : "Enemy")));
                Destroy(other.gameObject);
                break;


        }
    }
}
