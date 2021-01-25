using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private DepthValue currentDepth;
    [SerializeField] private DepthValue targetDepth;
    [SerializeField] private Canvas numberInventory;

	public void UpdateCurrentDepth(int value)
	{
        currentDepth.UpdateValue(value);
	}

}
