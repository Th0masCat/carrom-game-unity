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
        // Find the UpdatesText object and get the TextMeshProUGUI component
        popUpText = GameObject.Find("UpdatesText").GetComponent<TextMeshProUGUI>();
    }

    IEnumerator textPopUp(string text)
    {
        // Set the text and activate the UpdatesText object
        popUpText.text = text;
        popUpText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        // Deactivate the UpdatesText object after 3 seconds
        popUpText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Play audio when a coin/striker enters the pocket
        GetComponent<AudioSource>().Play();

        switch (other.gameObject.tag)
        {
            case "Striker":
                if (StrikerController.playerTurn == true)
                {
                    scorePlayer--; // Decrement the player's score by 1
                }
                else
                {
                    scoreEnemy--; // Decrement the enemy's score by 1
                }

                StartCoroutine(textPopUp("Striker Lost! -1 to " + (StrikerController.playerTurn ? "Player" : "Enemy"))); // Show the pop-up text with appropriate message
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero; // Set the velocity of the Striker to zero
                break;

            case "Black":
                scoreEnemy++; // Increment the enemy's score by 1

                StartCoroutine(textPopUp("Black Coin Entered! +1 to Enemy")); // Show the pop-up text with appropriate message
                Destroy(other.gameObject); // Destroy the collided Black coin
                break;

            case "White":
                scorePlayer++; // Increment the player's score by 1

                StartCoroutine(textPopUp("White Coin Entered! +1 to Player")); // Show the pop-up text with appropriate message
                Destroy(other.gameObject); // Destroy the collided White coin
                break;

            case "Queen":
                if (StrikerController.playerTurn == true)
                {
                    scorePlayer += 2; // Increment the player's score by 2
                }
                else
                {
                    scoreEnemy += 2; // Increment the enemy's score by 2
                }

                StartCoroutine(textPopUp("Queen Entered! +2 to " + (StrikerController.playerTurn ? "Player" : "Enemy"))); // Show the pop-up text with appropriate message
                Destroy(other.gameObject); // Destroy the collided Queen
                break;
        }
    }
}
