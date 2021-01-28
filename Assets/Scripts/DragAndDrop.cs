using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
	private Vector3 startPosition;
	private CanvasGroup canvasGroup;

	private RectTransform rectTransform;

	public Vector2 StartPosition { get => startPosition; set => startPosition = value; }

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
		//transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 wordPoint = new Vector3();
		RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, Input.mousePosition, Camera.main, out wordPoint);
		transform.position = wordPoint;
		//Debug.Log("OnDrag");
    }

	public void OnBeginDrag(PointerEventData eventData)
	{
		SetCanvasGroupBlockRaycast(false);
		StopAllCoroutines();
		//StopCoroutine(moveToStartPosition());
		//Debug.Log("OnBeginDrag");
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		SetCanvasGroupBlockRaycast(true);
		//Debug.Log("OnEndDrag");
		LerpToInitialPosition();
	}

	public void SetCanvasGroupBlockRaycast(bool value)
	{
		canvasGroup.blocksRaycasts = value;
	}

	public void LerpToInitialPosition()
	{
		StartCoroutine(moveToStartPosition());
		//LeanTween.move(gameObject, startPosition, 1f);
	}

	private IEnumerator moveToStartPosition()
	{
		//float t = .1f;
		//var currentPosition = rectTransform.anchoredPosition;
		//while(currentPosition != startPosition) { 
		//	rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, startPosition, t);
		//	currentPosition = rectTransform.anchoredPosition;
		//	yield return new WaitForFixedUpdate();
		//}

		float t = .1f;
		var currentPosition = transform.localPosition;
		while (currentPosition != startPosition)
		{
			transform.localPosition = Vector2.Lerp(transform.localPosition, startPosition, t);
			currentPosition = transform.localPosition;
			yield return new WaitForFixedUpdate();
		}
	}
}
