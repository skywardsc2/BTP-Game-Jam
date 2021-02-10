using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void OnSlotEmptyHandler();
public delegate void OnSlotPutHandler();
public class NumberSlot : MonoBehaviour
{
	private Number number;
	private bool isEmpty = true;
	public bool IsEmpty { get => isEmpty; set => isEmpty = value; }

	public Number Number
	{
		get => number; 
		
		set
		{
			number = value;
			if(number != null)
			{
				number.OnNumberConsumed += SlotEmpty;
				number.OnNumberReenabled += SlotPut;
				SlotPut();
			}
		}
	}

	public OnSlotEmptyHandler OnSlotEmpty;
	public OnSlotPutHandler OnSlotPut;

	public void Clear()
	{
		if(Number != null)
			Destroy(Number.gameObject);
		SlotEmpty();
	}

	public void SlotEmpty()
	{
		IsEmpty = true;
		OnSlotEmpty?.Invoke();
	}

	public void SlotPut()
	{
		IsEmpty = false;
		OnSlotPut?.Invoke();
	}

	private void Update()
	{
		//Debug.Log(GetComponent<RectTransform>().localP);
	}
}
