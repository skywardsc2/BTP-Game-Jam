using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class ShowHide : MonoBehaviour
{
	public enum Transition{
		Move,
		Fade
	};

	public Transition transitionType;

	public float showFadeValue;
	public float hideFadeValue;
	public Transform showPosition;
	public Transform hidePosition;

	public float time;
	public LeanTweenType easeType;
	public bool disableAfterHide;

	private CanvasGroup canvasGroup;

	private void Awake()
	{
		canvasGroup = GetComponent<CanvasGroup>();

		//Vector3 adjustScreenValues = new Vector3(Camera.main.pixelWidth - 1920, Camera.main.pixelHeight - 1080, 0);
		//showPosition.position += adjustScreenValues;
		//hidePosition.position += adjustScreenValues;
	}

	public void Show()
	{
		if(transitionType == Transition.Move)
		{
			LeanTween.move(gameObject, showPosition, time).setEase(easeType);
		} else if (transitionType == Transition.Fade)
		{
			if(canvasGroup != null)
				LeanTween.alphaCanvas(canvasGroup, showFadeValue, time).setEase(easeType);
			//else
			//{
			//	throw new MissingComponentException();
			//}
		}
	}

	public void Hide()
	{
		if (transitionType == Transition.Move)
		{
			if (disableAfterHide)
			{
				LeanTween.move(gameObject, hidePosition, time).setEase(easeType).setOnComplete(() => gameObject.SetActive(false));
			} else
			{
				LeanTween.move(gameObject, hidePosition, time).setEase(easeType);
			}
		}
		else if (transitionType == Transition.Fade)
		{
			if (canvasGroup)
			{
				if (disableAfterHide)
					LeanTween.alphaCanvas(canvasGroup, hideFadeValue, time).setEase(easeType).setOnComplete(() => gameObject.SetActive(false));
				else
					LeanTween.alphaCanvas(canvasGroup, hideFadeValue, time).setEase(easeType);
			}
			else
			{
				throw new MissingComponentException();
			}
		}

		if (disableAfterHide)
			gameObject.SetActive(false);
	}
}
