using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public delegate void OnNumberConsumedHandler();
public delegate void OnNumberReenabledHandler();

public class Number : MonoBehaviour
{
	[HideInInspector] public TextMeshProUGUI UIText;
	private int numberValue;

	public event OnNumberConsumedHandler OnNumberConsumed;
	public event OnNumberReenabledHandler OnNumberReenabled;

	public DragAndDrop dragAndDropComponent;

	public int NumberValue
	{
		get
		{
			return numberValue;
		}

		set
		{
			numberValue = value;
			UIText.SetText(numberValue.ToString());
		}
	}

	private void Awake()
	{
		UIText = GetComponentInChildren<TextMeshProUGUI>();
		dragAndDropComponent = GetComponent<DragAndDrop>();
	}

	private void Start()
	{
		float cameraHeight = Camera.main.scaledPixelHeight;
		Vector3 initialPosition = new Vector3(transform.position.x, transform.position.y + cameraHeight, transform.position.z);
		transform.position = initialPosition;
		dragAndDropComponent.LerpToInitialPosition();
	}

	public void Consume()
	{
		OnNumberConsumed?.Invoke();
		this.gameObject.SetActive(false);
	}

	public void Reenable()
	{
		this.gameObject.SetActive(true);
		dragAndDropComponent.LerpToInitialPosition();
		dragAndDropComponent.SetCanvasGroupBlockRaycast(true);
		OnNumberReenabled?.Invoke();
	}
}
