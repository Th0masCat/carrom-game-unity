using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public static int scorePlayer1 = 0;
    public static int scorePlayer2 = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Striker":
                if (StrikerController.playerTurn == true)
                {
                    scorePlayer1--;
                }
                else
                {
                    scorePlayer2--;
                }

                Debug.Log("Striker Entered");
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case "Black":
                scorePlayer1++;

                Debug.Log("Black Entered");
                Destroy(other.gameObject);
                break;
            case "White":
                scorePlayer2++;
                Debug.Log("White Entered");
                Destroy(other.gameObject);
                break;
            case "Queen":

                if (StrikerController.playerTurn == true)
                {
                    scorePlayer1 += 2;
                }
                else
                {
                    scorePlayer2 += 2;
                }
                Debug.Log("Queen Entered");
                Destroy(other.gameObject);
                break;


        }
    }
}
