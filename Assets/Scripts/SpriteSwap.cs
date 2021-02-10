using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwap : MonoBehaviour
{
    public Sprite spriteOn, spriteOff;

	private Image image;
	private Sprite current, other;

	private void Start()
	{
		image = GetComponent<Image>();
		current = spriteOn;
		other = spriteOff;
		image.sprite = current;
	}

	public void SwapSprites()
	{
		image.sprite = other;
		other = current;
		current = image.sprite;
	}
}
