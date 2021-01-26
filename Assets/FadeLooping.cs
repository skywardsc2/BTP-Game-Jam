using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeLooping : MonoBehaviour
{
    public float targetAlpha;
    public float time;
    public LeanTweenType easeType;

    private LTDescr descr;

    void Start()
    {
        descr = LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), targetAlpha, time).setEase(easeType).setLoopPingPong();
    }

	public void Stop()
	{
        descr.pause();
	}
}
