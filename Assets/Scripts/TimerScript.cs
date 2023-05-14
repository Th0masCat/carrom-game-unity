using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI timerText;

    public float timeLeft = 120.0f;  // The time in seconds that the timer will run for

    public bool isTimerRunning = true;  // Indicates whether the timer is currently running

    void Update()
    {
        if (isTimerRunning)
        {
            timeLeft -= Time.deltaTime;  // Decrement the time left by the amount of time that has passed since the last frame
            timerText.text = Mathf.Round(timeLeft).ToString();  // Update the text to show the time left

            if (timeLeft <= 0)
            {
                // The timer has run out
                isTimerRunning = false;
                timerText.text = "Time's Up!";
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        timeLeft = 120.0f;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

}
