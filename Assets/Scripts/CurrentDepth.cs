using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentDepth : DepthValue
{
	public int currentBaseValue;

	protected override void Awake()
	{
		base.Awake();

		UpdateValue(currentValue);
		currentBaseValue = currentValue;
	}

	public void UpdateBaseValue(int value)
	{
		currentBaseValue = value;
	}
}
