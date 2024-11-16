using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10f;
    public bool timerIsRunning = false;
    public Text timeText;

    public UnityEvent OnTimerEnd;

    private void Start()
    {
        // Démarre le minuteur
        timerIsRunning = true;
    }

    private void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}