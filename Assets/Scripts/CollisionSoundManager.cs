using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundManager : MonoBehaviour
{

    public static bool shouldBeStatic = true;
    void Update(){
        if(shouldBeStatic){
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }else{
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > 0.1f)
        {
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().volume = other.relativeVelocity.magnitude / 10;
        }
    }
}
