using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameState : MonoBehaviour
{
    public UnityEvent OnGameLoad;
    public UnityEvent OnGameStart;
    public UnityEvent OnGameEnd;
    public UnityEvent OnGameRestart;

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
}
