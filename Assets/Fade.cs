using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class Fade : MonoBehaviour
{
	public float to;
	public float time;
	public LeanTweenType easeType;

	public void FadeUI()
	{
		LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), to, time).setEase(easeType);
	}
}
