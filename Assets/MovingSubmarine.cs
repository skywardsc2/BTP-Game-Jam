using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingSubmarine : MonoBehaviour
{
    public float distance;
	public float moveTime = 1;
	public LeanTweenType easeType;

	[HideInInspector] public Vector3 initialPosition;
	public SpriteRenderer bgSpriteRenderer;
	public GameState gameState;

	private void Start()
	{
		var bounds = bgSpriteRenderer.sprite.bounds;
		var spriteHeight = bounds.size.y;
		var spriteWidth = bounds.size.x;

		float xPos = bgSpriteRenderer.transform.position.x;
		float yPos = bgSpriteRenderer.transform.position.y + spriteHeight / 2 - Camera.main.orthographicSize;

		transform.position = new Vector3((float)xPos, yPos, transform.position.z);
		initialPosition = transform.position;
		CalculateDistanceToMove();
	}

	private void CalculateDistanceToMove()
	{
		//float textureHeight = spriteRenderer.sprite.rect.height;
		//float pixelsPerUnit = spriteRenderer.sprite.pixelsPerUnit;
		//float maxTravelDistance = (textureHeight/pixelsPerUnit) - Camera.main.scaledPixelHeight;
		//distance = textureHeight / ((gameState.confirmsToWin) * pixelsPerUnit);

		//Debug.Log("( " + textureHeight + " - " + Camera.main.scaledPixelHeight + " ) / ( " + gameState.confirmsToWin + " * " + pixelsPerUnit + " ) = " + distance);

		var bounds = bgSpriteRenderer.sprite.bounds;
		var spriteHeight = bounds.size.y;
		var maxTravelDistance = spriteHeight - 2 * Camera.main.orthographicSize;

		distance = maxTravelDistance / gameState.confirmsToWin;

		Debug.Log("( " + spriteHeight + " - 2 * " + Camera.main.orthographicSize + " ) / ( " + gameState.confirmsToWin + " ) = " + distance);
	}

	public void MoveDown()
	{
		//Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
		//transform.position = newPosition;

		LeanTween.moveY(gameObject, transform.position.y - distance, moveTime).setEase(easeType);
	}

	public void MoveBackToStart()
	{
		LeanTween.moveY(gameObject, initialPosition.y, moveTime).setEase(easeType);
	}
}
