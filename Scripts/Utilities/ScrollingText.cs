using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingText : MonoBehaviour
{
	[Header("Setup")]
	[SerializeField] private RectTransform canvasTransform = null;
	private RectTransform thisTransform = null;
	private Text thisText = null;
	private Coroutine currentScroll = null;

	[Header("Animation")]
	[SerializeField] private float duration = 2.0f;
	[SerializeField] private AnimationCurve speedCurve;

	
	private void Awake()
	{
		thisTransform = GetComponent<RectTransform>();
		thisText = GetComponent<Text>();
	}


	public void Scroll(string text)
	{
		if (currentScroll != null)
		{
			StopCoroutine(currentScroll);
		}

		thisText.text = text;

		float offset = thisText.preferredWidth / 2;
		float canvasWidth = Screen.width / canvasTransform.localScale.x;

		Vector2 startPos = new Vector2(0.0f - offset, thisTransform.anchoredPosition.y);
		Vector2 endPos = new Vector2(canvasWidth + offset, thisTransform.anchoredPosition.y);

		thisTransform.anchoredPosition = startPos;

		currentScroll = StartCoroutine(Move(startPos, endPos, speedCurve, duration));
	}


	IEnumerator Move(Vector2 start, Vector2 end, AnimationCurve curve, float time)
	{
		float timer = 0.0f;

		while (timer < time)
		{
			timer += Time.deltaTime;

			if (timer > time)
			{
				timer = time;
			}

			thisTransform.anchoredPosition = Vector2.Lerp(start, end, curve.Evaluate(timer / time));

			yield return null;
		}
	}
}
