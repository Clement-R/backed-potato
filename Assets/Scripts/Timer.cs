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
            }
            else
            {
                timeRemaining = 0;
                timerIsRunning = false;
                OnTimerEnd?.Invoke();
            }
        }
    }
}