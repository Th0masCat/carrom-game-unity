using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundManager : MonoBehaviour
{
    public static bool shouldBeStatic = true;

    void Update()
    {
        // Check if the object should be static or dynamic and update its Rigidbody2D body type accordingly.
        if (shouldBeStatic)
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
        else
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // Check if the collision is not with a "Pocket" object and the relative velocity magnitude is greater than 0.1f.
        if (!other.gameObject.CompareTag("Pocket") && other.relativeVelocity.magnitude > 0.1f)
        {
            // Play the collision sound and adjust the volume based on the relative velocity magnitude.
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().volume = other.relativeVelocity.magnitude / 10;
        }
    }
}
