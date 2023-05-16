using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public static int scoreEnemy = 0;
    public static int scorePlayer = 0;

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

                Debug.Log("Striker Entered");
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case "Black":
                scoreEnemy++;

                Debug.Log("Black Entered");
                Destroy(other.gameObject);
                break;
            case "White":
                scorePlayer++;
                Debug.Log("White Entered");
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
                Debug.Log("Queen Entered");
                Destroy(other.gameObject);
                break;


        }
    }
}
