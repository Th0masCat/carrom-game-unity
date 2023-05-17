using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundManager : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 0.1f)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().volume = other.relativeVelocity.magnitude / 10;
            Debug.Log(other.relativeVelocity.magnitude / 10);
        }
    }
}
