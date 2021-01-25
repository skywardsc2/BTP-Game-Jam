using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void OnValueUpdateHandler();

public abstract class DepthValue : MonoBehaviour
{
	[HideInInspector] public int currentValue;
	[HideInInspector] public int initialValue;
	[HideInInspector] public TextMeshProUGUI UIText;

	public event OnValueUpdateHandler OnValueUpdate;

	protected virtual void Awake()
	{
		UIText = GetComponent<TextMeshProUGUI>();
		currentValue = initialValue;
	}

	public void UpdateValue(int newValue)
	{
		currentValue = newValue;
		UIText.SetText(newValue.ToString());
		OnValueUpdate?.Invoke();
	}
}
