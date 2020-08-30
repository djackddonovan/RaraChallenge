using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{

	public Button stopButton;
	public Text scoreText;

	void Awake()
	{
		stopButton.onClick.AddListener(Stop);
	}

	void Stop()
	{
		GameManager.Instance.EndPlayMode();
	}

	void LateUpdate()
	{
		scoreText.text = PlayData.Instance.Score.ToString();	
	}

}
