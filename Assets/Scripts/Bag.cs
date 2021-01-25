using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public delegate void OnNumberDropHandler(Number number, int multiplier);

public class Bag : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	private Image image;
	private Color initialColor;

	public int valueMultiplier = -1;
	public CurrentDepth currentDepth;

	public event OnNumberDropHandler OnNumberDrop;

	private void Start()
	{
		image = GetComponentInChildren<Image>();
		initialColor = image.color;
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (eventData.pointerDrag != null)
		{
			Number number = eventData.pointerDrag.GetComponent<Number>();
			if(number != null)
			{
				var newValue = currentDepth.currentValue + number.NumberValue * valueMultiplier;
				OnNumberDrop?.Invoke(number, valueMultiplier);
				number.Consume();
				currentDepth.UpdateValue(newValue);
			}
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if(eventData.dragging)
			image.color = new Color(255, 255, 0);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		image.color = initialColor;
	}
}
