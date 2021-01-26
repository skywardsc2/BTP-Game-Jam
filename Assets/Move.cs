using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Move : MonoBehaviour
{
	public Transform to;
	public float time;
	public LeanTweenType easeType;

	public void MoveUI()
	{
		LeanTween.move(gameObject, to, time).setEase(easeType);
	}
}
