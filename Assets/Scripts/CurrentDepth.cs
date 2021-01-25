using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentDepth : DepthValue
{
	protected override void Awake()
	{
		base.Awake();

		UpdateValue(currentValue);
	}
}
