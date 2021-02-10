using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
	[HideInInspector] public CurrentDepth currentDepth;

	private TextMeshProUGUI textGUI;

	private void Awake()
	{
		currentDepth = GameObject.FindGameObjectWithTag("CurrentDepth").GetComponent<CurrentDepth>();
		textGUI = GetComponent<TextMeshProUGUI>();
		textGUI.SetText($"YOU DIVED {-1 * currentDepth.currentBaseValue:D3} METERS");
	}
	public void UpdateScoreText()
	{
		textGUI.SetText($"YOU DIVED {-1 * currentDepth.currentBaseValue} METERS");
	}
}
