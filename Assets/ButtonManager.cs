using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameObject UndoButton;
    public GameObject ConfirmButton;
	[HideInInspector] public DepthValue CurrentDepth;
	[HideInInspector] public DepthValue TargetDepth;
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

		CurrentDepth = GameObject.FindGameObjectWithTag(CurrentDepthTag).GetComponent<DepthValue>();
		TargetDepth = GameObject.FindGameObjectWithTag(TargetDepthTag).GetComponent<DepthValue>();
		timer = GameObject.FindGameObjectWithTag(TimerTag).GetComponent<Timer>();

		CurrentDepth.OnValueUpdate += OnCurrentDepthUpdate;
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
		if(CurrentDepth.currentValue == TargetDepth.currentValue && NumberInventory.NumberCount == 0)
		{
			//Debug.Log("OnCurrentDepthUpdate called");
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
