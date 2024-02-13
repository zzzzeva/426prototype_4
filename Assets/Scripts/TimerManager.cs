using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TimerManager : MonoBehaviour
{
    public float[] countdownTimes; // Array of times to set the countdown
    private int currentTimeIndex = 0; // Index to track the current time set
    private float timeRemaining; // Time remaining in the countdown
    private bool timerIsRunning = false; // Is the timer currently running?
    public TextMeshProUGUI timeDisplay;
    public Switch switchChange;

    private void Start()
    {
        StartOrResetTimer();
        timeDisplay = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                // Update the time remaining
                timeRemaining -= Time.deltaTime;
                timeDisplay.text = (int)Mathf.Ceil(timeRemaining) + " seconds remaining";
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                // Here, you can add code to end the game
                EndGame();
            }
        }
    }

    // Method to start or reset the timer with a new duration from the array
    public void StartOrResetTimer()
    {
        if (currentTimeIndex < countdownTimes.Length)
        {
            timeRemaining = countdownTimes[currentTimeIndex];
            timerIsRunning = true;
            currentTimeIndex++;
        }
        else
        {
            timerIsRunning = false;
            timeDisplay.text = "YOU WIN";
            switchChange.WinGame();
        }
    }

    void EndGame()
    {
        // Implement what happens when the game ends
        timeDisplay.text = "Game Over.";
        switchChange.EndGame();
    }
}

