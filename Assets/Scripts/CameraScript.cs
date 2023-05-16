using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer boardSprite;
	void Start () {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = boardSprite.bounds.size.x / boardSprite.bounds.size.y;

        if(screenRatio >= targetRatio){
            Camera.main.orthographicSize = boardSprite.bounds.size.y / 2;
        }else{
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = boardSprite.bounds.size.y / 2 * differenceInSize;
        }
	}
}
