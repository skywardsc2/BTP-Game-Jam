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
	public NumberInventory NumberInventory;

	public string CurrentDepthTag;
	public string TargetDepthTag;

	private GameObject activeButton;
	private GameObject inactiveButton;

	private void Start()
	{
		activeButton = UndoButton;
		activeButton.SetActive(true);
		inactiveButton = ConfirmButton;
		inactiveButton.SetActive(false);

		CurrentDepth = GameObject.FindGameObjectWithTag(CurrentDepthTag).GetComponent<DepthValue>();
		TargetDepth = GameObject.FindGameObjectWithTag(TargetDepthTag).GetComponent<DepthValue>();

		CurrentDepth.OnValueUpdate += OnCurrentDepthUpdate;
	}

	private void OnCurrentDepthUpdate()
	{
		Debug.Log("NumberInventory: " + NumberInventory.NumberCount);
		if(CurrentDepth.currentValue == TargetDepth.currentValue && NumberInventory.NumberCount == 0)
		{
			SwapButtons();
		}
	}

	public void SwapButtons()
	{
		activeButton.SetActive(false);
		inactiveButton.SetActive(true);
		var button = inactiveButton;
		inactiveButton = activeButton;
		activeButton = button;
	}
}
