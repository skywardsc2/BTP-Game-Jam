using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private Vector2 startPosition = new Vector2(0, 0);
	private CanvasGroup canvasGroup;

	private RectTransform rectTransform;

	private void Start()
	{
		canvasGroup = GetComponent<CanvasGroup>();
		rectTransform = GetComponent<RectTransform>();
		//startPosition = rectTransform.anchoredPosition;
		//Debug.Log(rectTransform.anchoredPosition + " " + rectTransform.position + " " + rectTransform.localPosition);
	}

	public void OnDrag(PointerEventData eventData)
	{
		//StopCoroutine(moveToStartPosition());
		transform.position = Input.mousePosition;
		Debug.Log("OnDrag");
    }

	public void OnBeginDrag(PointerEventData eventData)
	{
		SetCanvasGroupBlockRaycast(false);
		StopAllCoroutines();
		//StopCoroutine(moveToStartPosition());
		Debug.Log("OnBeginDrag");
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		SetCanvasGroupBlockRaycast(true);
		Debug.Log("OnEndDrag");
		LerpToInitialPosition();
	}

	public void SetCanvasGroupBlockRaycast(bool value)
	{
		canvasGroup.blocksRaycasts = value;
	}

	public void LerpToInitialPosition()
	{
		StartCoroutine(moveToStartPosition());
	}

	private IEnumerator moveToStartPosition()
	{
		float t = .1f;
		var currentPosition = rectTransform.anchoredPosition;
		while(currentPosition != startPosition) { 
			rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, startPosition, t);
			currentPosition = rectTransform.anchoredPosition;
			yield return new WaitForFixedUpdate();
		}
	}
}
