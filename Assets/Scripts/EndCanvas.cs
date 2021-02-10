using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EndCanvas : MonoBehaviour
{
	[HideInInspector] public CurrentDepth currentDepth;

	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI endMessage;
	public Button tryAgainButton;

	public GameState gameState;

	private void Awake()
	{
		currentDepth = GameObject.FindGameObjectWithTag("CurrentDepth").GetComponent<CurrentDepth>();
		scoreText.SetText($"YOU DIVED {-1 * currentDepth.currentBaseValue:D3} METERS");
	}
	public void SetupEndCanvas()
	{
		scoreText.SetText($"YOU DIVED {-1 * currentDepth.currentBaseValue} METERS");
		if (gameState.playerWon)
		{
			endMessage.SetText("CONGRATULATIONS!");
			tryAgainButton.GetComponentInChildren<TextMeshProUGUI>().SetText("TRY AGAIN");
		} else
		{
			endMessage.SetText("BETTER LUCK NEXT TIME!");
			tryAgainButton.GetComponentInChildren<TextMeshProUGUI>().SetText("TRY AGAIN");
		}
	}
}
