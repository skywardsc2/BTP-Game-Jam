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
	private Animator animator;

	public int valueMultiplier = -1;
	public string animatorOpenParameterName;
	public CurrentDepth currentDepth;

	public event OnNumberDropHandler OnNumberDrop;

	private void Start()
	{
		image = GetComponentInChildren<Image>();
		animator = GetComponentInChildren<Animator>();
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
		Debug.Log("Mouse Entered Bag");
		if (eventData.dragging)
			animator.SetBool(animatorOpenParameterName, true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("Mouse Left Bag");
		animator.SetBool(animatorOpenParameterName, false);
	}
}
