using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public UnityEvent OnGameLoad;
    public UnityEvent OnGameStart;
	public UnityEvent OnConfirmPressed;
    public UnityEvent OnGameEnd;
    public UnityEvent OnGameRestart;

	[HideInInspector] public int confirmTimes = 0;
	public int confirmsToWin = 10;

	public bool playerWon = false;

	private void Start()
	{
		LoadGame();
	}

	public void StartGame()
	{
        OnGameStart.Invoke();
	}

    public void LoadGame()
	{
        OnGameLoad.Invoke();
	}

    public void EndGame()
	{
        OnGameEnd.Invoke();
	}

    public void RestartGame()
	{
        OnGameRestart.Invoke();
	}

	public void IncreaseConfirmTimes()
	{
		confirmTimes++;
		if (confirmTimes == confirmsToWin)
		{
			playerWon = true;
			EndGame();
		} else
		{
			OnConfirmPressed?.Invoke();
		}
	}

	public void ResetConfirmTimes()
	{
		playerWon = false;
		confirmTimes = 0;
	}
}
