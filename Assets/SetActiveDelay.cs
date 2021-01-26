using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveDelay : MonoBehaviour
{
    public float delay;
	public bool value;

    public void SetActiveDelayMethod()
	{
		StartCoroutine(SetActiveDelayCoroutine());
		
	}

	private IEnumerator SetActiveDelayCoroutine()
	{
		yield return new WaitForSeconds(delay);
		gameObject.SetActive(value);
	}
}
