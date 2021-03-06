using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLooping : MonoBehaviour
{
    public float scaleFactor;
    public float time;
    public LeanTweenType easeType;

    private Vector3 to;
    private LTDescr descr;
    
    private void Start()
    {
        to = gameObject.transform.localScale * scaleFactor;
        descr = LeanTween.scale(gameObject, to, time).setEase(easeType).setLoopPingPong();
    }

    public void PausePulsing()
	{
        descr.pause();
	}
}
