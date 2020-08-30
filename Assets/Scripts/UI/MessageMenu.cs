using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageMenu : Singleton<MessageMenu>
{

	public AnimationCurve deploymentCurve;

	public Text text;

	ContentSizeFitter sizeFitter;

	float timer;

	float height;
	float curveDuration;

	void Awake()
	{
		height = (transform as RectTransform).rect.height;
		(transform as RectTransform).anchoredPosition = new Vector2(0f, -height);
		curveDuration = deploymentCurve.keys[deploymentCurve.length - 1].time;

		sizeFitter = GetComponent<ContentSizeFitter>();
	}

	public static void Show(string _text)
	{
		Instance.InnerShow(_text);
	}

	void InnerShow(string _text)
	{
		text.text = _text;
		timer = curveDuration;

		// size fitter doesn't properly automatically update when a child text is changed
		sizeFitter.enabled = false;
		sizeFitter.enabled = true;
		
		var grp = GetComponent<LayoutGroup>();
		grp.enabled = false;
		grp.enabled = true;
		
		grp.CalculateLayoutInputHorizontal();
		grp.CalculateLayoutInputVertical();
		grp.SetLayoutHorizontal();
		grp.SetLayoutVertical();
	}

	void Update()
	{
		if (timer > 0f)
		{
			timer = Mathf.Max(0f, timer - Time.deltaTime);
			float t = 1f - deploymentCurve.Evaluate(curveDuration - timer);
			(transform as RectTransform).anchoredPosition = new Vector2(0f, -height * t);
		}
	}

}
