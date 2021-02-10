using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject UndoButton;
    public GameObject ConfirmButton;
	[HideInInspector] public CurrentDepth currentDepth;
	[HideInInspector] public TargetDepth targetDepth;
	[HideInInspector] public Timer timer;
	public NumberInventory NumberInventory;

	public string CurrentDepthTag;
	public string TargetDepthTag;
	public string TimerTag;

	//private GameObject activeButton;
	//private GameObject inactiveButton;

	private void Start()
	{
		ResetButtons();

		currentDepth = GameObject.FindGameObjectWithTag(CurrentDepthTag).GetComponent<CurrentDepth>();
		targetDepth = GameObject.FindGameObjectWithTag(TargetDepthTag).GetComponent<TargetDepth>();
		timer = GameObject.FindGameObjectWithTag(TimerTag).GetComponent<Timer>();

		currentDepth.OnValueUpdate += OnCurrentDepthUpdate;
	}

	public void ResetButtons()
	{
		//activeButton = UndoButton;
		UndoButton.SetActive(true);
		//inactiveButton = ConfirmButton;
		ConfirmButton.SetActive(false);
	}

	private void OnCurrentDepthUpdate()
	{
		if(currentDepth.currentValue == targetDepth.currentValue && NumberInventory.NumberCount == 0)
		{
			//Debug.Log("OnCurrentDepthUpdate called");
			currentDepth.UpdateBaseValue(targetDepth.currentValue);
			SwapButtons();
			timer.Pause();
		}
	}

	public void SwapButtons()
	{
		//activeButton.SetActive(false);
		//inactiveButton.SetActive(true);
		//var button = inactiveButton;
		//inactiveButton = activeButton;
		//activeButton = button;
		UndoButton.SetActive(false);
		ConfirmButton.SetActive(true);
	}
}
