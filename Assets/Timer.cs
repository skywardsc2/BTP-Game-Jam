using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    public float initalMaxTime = 59;
    public float currentMaxTime = 59;
    public float maxTimeDecreaseAmount = 10;
    public float timeRemaining = 10;
    public GameState gameState;

    public List<int> decreaseMaxTimeOn;
    private int decreaseMaxTimeIndex = 0;
    private int timerSetupCounter = 0;

    public bool timerIsRunning = false;
    
    private TextMeshProUGUI timeText;

    private void Start()
    {
        // Starts the timer automatically
        timeText = GetComponent<TextMeshProUGUI>();

        ResetTimer();
    }

	void Update()
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
                gameState.EndGame();
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.SetText(string.Format("{0:00}:{1:00}", minutes, seconds));
    }

	public void SetupTimer()
	{
        timerSetupCounter++;
        if(decreaseMaxTimeOn != null && decreaseMaxTimeIndex < decreaseMaxTimeOn.Count && timerSetupCounter == decreaseMaxTimeOn[decreaseMaxTimeIndex])
		{
            decreaseMaxTimeIndex++;
            currentMaxTime -= maxTimeDecreaseAmount;
		}

        timeRemaining = currentMaxTime;
        timerIsRunning = true;
	}

    public void ResetTimer()
	{
        timerIsRunning = true;
        timeRemaining = initalMaxTime;
        currentMaxTime = initalMaxTime;

        timerSetupCounter = 0;
        decreaseMaxTimeIndex = 0;
    }

    public void Pause()
	{
        timerIsRunning = false;
	}

    public void Resume()
	{
        timerIsRunning = true;
	}
}
