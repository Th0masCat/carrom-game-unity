using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer boardSprite;

    void Start()
    {
        // Calculate the screen ratio and the target ratio of the board sprite.
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = boardSprite.bounds.size.x / boardSprite.bounds.size.y;

        // Adjust the camera's orthographic size based on the screen and target ratios.
        if (screenRatio >= targetRatio)
        {
            // If the screen ratio is larger or equal to the target ratio, set the orthographic size based on the board sprite's height.
            Camera.main.orthographicSize = boardSprite.bounds.size.y / 2;
        }
        else
        {
            // If the screen ratio is smaller than the target ratio, calculate the difference in size and adjust the orthographic size accordingly.
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = boardSprite.bounds.size.y / 2 * differenceInSize;
        }
    }
}
