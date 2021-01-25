using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TargetDepth : DepthValue
{
	public NumberInventory numberInventory;
	public DepthValue currentDepthValue;
	protected override void Awake()
	{
		base.Awake();
		GenerateNewTargetDepthValue();
		initialValue = currentValue;
	}

	public void GenerateNewTargetDepthValue()
	{
		UpdateValue(currentDepthValue.currentValue - Random.Range(80, 100));
	}
}
