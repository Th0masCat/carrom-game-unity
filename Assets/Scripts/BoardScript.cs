using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Striker":
                Debug.Log("Striker Entered");
                other.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                break;
            case "Black":
                Debug.Log("Black Entered");
                Destroy(other.gameObject);
                break;
            case "White":
                Debug.Log("White Entered");
                Destroy(other.gameObject);
                break;
            case "Queen":
                Debug.Log("Queen Entered");
                Destroy(other.gameObject);
                break;


        }
    }
}
