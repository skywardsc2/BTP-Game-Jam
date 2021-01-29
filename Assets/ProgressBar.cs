using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public MovingSubmarine movingSubmarine;
	public SpriteRenderer bgSpriteRenderer;
	public float iconMarginFromBarTop = 5;

    public Image bar;
    public Image subIcon;

	private float iconInitialPosition;
	private float scale = 0.5f;

	private void Start()
	{
		var barHeight = bar.GetComponent<RectTransform>().sizeDelta.y;
		var bgHeight = bgSpriteRenderer.sprite.bounds.size.y;
		scale = (barHeight - 2 * iconMarginFromBarTop) / bgHeight;

		Debug.Log("scale = " + scale + " barHeight = " + barHeight + " bgHeight = " + bgHeight);

		var xPos = bar.transform.localPosition.x;
		var yPos = bar.transform.localPosition.y + (barHeight - 2 * iconMarginFromBarTop)/2;
		subIcon.transform.localPosition = new Vector3(xPos, yPos, bar.transform.localPosition.z);

		movingSubmarine.OnSubmarineMoved += MoveDown;
	}

	public void MoveDown(float distance, float moveTime, LeanTweenType easeType)
	{
		Debug.Log("local y = " + subIcon.transform.localPosition.y + " scale = " + scale + " distance = " + distance);
		LeanTween.moveLocalY(subIcon.gameObject, subIcon.transform.localPosition.y + scale * distance, moveTime).setEase(easeType);
	}
}
